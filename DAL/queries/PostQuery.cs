namespace dal.queries
{
    public class PostQuery
    {
        public string get_post_by_id() => @"SELECT * FROM [post] WHERE post_id = @post_id;";

        public string add_post() => @"
        INSERT INTO post (post_id, user_id, content, created_at)
        OUTPUT INSERTED.post_id
        VALUES (@post_id, @user_id, @content, @created_at);
        ";

        public string like_post() => @"
        INSERT INTO post_like (post_id, user_id, created_at)
        VALUES (@post_id, @user_id, @created_at);
        ";

        public string dislike_post() => @"
        DELETE FROM post_like
        WHERE post_id = @post_id
            AND user_id = @user_id;";

        public string delete_post_by_id() => @"DELETE FROM [post] WHERE post_id = @post_id;";

        public string add_post_media() => @"
        INSERT INTO post_media (media_id, post_id, media_type, media_src)
        VALUES (@media_id ,@post_id, @media_type, @media_src);";

        public string delete_post_like() => @"
        DELETE FROM post_like
        WHERE post_id = @post_id
            AND user_id = @user_id;";

        public string get_home_init() => @"
        -- Declare requester ID
        DECLARE @req_user_id UNIQUEIDENTIFIER = @user_id;

        WITH PostStats AS (
            SELECT
                p.post_id,
                p.user_id,
                p.content,
                p.edited,
                p.edited_at,
                p.created_at,
                ISNULL(likes.like_count, 0) AS like_count,
                ISNULL(comments.comment_count, 0) AS comment_count,
                CASE
                    WHEN f.friendship_id IS NOT NULL THEN 1
                    ELSE 0
                END AS is_friend
            FROM post p
            LEFT JOIN (
                SELECT post_id, COUNT(*) AS like_count
                FROM post_like
                GROUP BY post_id
            ) likes ON likes.post_id = p.post_id
            LEFT JOIN (
                SELECT post_id, COUNT(*) AS comment_count
                FROM comment
                WHERE parent_id IS NULL
                GROUP BY post_id
            ) comments ON comments.post_id = p.post_id
            LEFT JOIN friend f ON (
                (f.user_1_id = @req_user_id AND f.user_2_id = p.user_id OR
                f.user_2_id = @req_user_id AND f.user_1_id = p.user_id)
                AND f.status = 'accepted'
            )
        ),
        RankedPosts AS (
            SELECT TOP 7 *
            FROM PostStats
            ORDER BY
                is_friend DESC,
                like_count DESC,
                comment_count DESC,
                created_at DESC
        ),
        RandomPosts AS (
            SELECT TOP 3 *
            FROM PostStats
            ORDER BY NEWID()
        ),
        CombinedPosts AS (
            SELECT * FROM RankedPosts
            UNION
            SELECT * FROM RandomPosts
        ),
        MediaCTE AS (
            SELECT
                pm.post_id,
                (
                    SELECT
                        pm_inner.media_id,
                        pm_inner.media_type,
                        pm_inner.media_src,
                        pm_inner.[order]
                    FROM post_media pm_inner
                    WHERE pm_inner.post_id = pm.post_id
                    ORDER BY pm_inner.[order]
                    FOR JSON PATH
                ) AS media_json
            FROM post_media pm
            GROUP BY pm.post_id
        ),
        UserInfoCTE AS (
            SELECT
                u.user_id,
                u.username,
                u.pfp_src,
                u.is_private
            FROM [user] u
        ),
        RequesterLikesCTE AS (
            SELECT
                pl.post_id,
                1 AS is_liked_requester
            FROM post_like pl
            WHERE pl.user_id = @req_user_id
        )

        -- Final Selection with full fields
        SELECT
            cp.post_id,
            cp.user_id,
            ISNULL(u.username, '') AS username,
            ISNULL(u.pfp_src, '') AS pfp_src,
            ISNULL(u.is_private, '') AS is_private,
            cp.content,
            cp.edited,
            cp.edited_at,
            cp.created_at,
            cp.like_count,
            cp.comment_count,
            cp.is_friend,
            ISNULL(m.media_json, '[]') AS media_json,
            CASE
                WHEN cp.user_id = @req_user_id THEN NULL
                ELSE ISNULL(rl.is_liked_requester, 0)
            END AS is_liked_requester
        FROM CombinedPosts cp
        LEFT JOIN UserInfoCTE u ON cp.user_id = u.user_id
        LEFT JOIN MediaCTE m ON cp.post_id = m.post_id
        LEFT JOIN RequesterLikesCTE rl ON cp.post_id = rl.post_id
        ORDER BY
            cp.is_friend DESC,
            cp.like_count DESC,
            cp.comment_count DESC,
            cp.created_at DESC;

        ";

        // public string get_first_10_private_posts() => @"
        //     WITH FriendsCTE AS (
        //         SELECT
        //             CASE
        //                 WHEN f.user_1_id = @user_id THEN f.user_2_id
        //                 ELSE f.user_1_id
        //             END AS friend_user_id
        //         FROM friend f
        //         WHERE (f.user_1_id = 1 OR f.user_2_id = @user_id)
        //             AND f.status = 'accepted'
        //     ),
        //     LatestFriendPostCTE AS (
        //         SELECT
        //             p.post_id,
        //             p.title as post_tile,
        //             p.user_id AS friend_user_id,
        //             p.content AS post_content,
        //             p.has_img as post_has_img,
        //             p.media_src as post_media_src,
        //             p.created_at AS post_created_at,
        //             ROW_NUMBER() OVER(PARTITION BY p.user_id ORDER BY p.created_at DESC) as rn
        //         FROM post p
        //         JOIN FriendsCTE f ON p.user_id = f.friend_user_id
        //     ),
        //     PostLikesCTE AS (
        //         SELECT
        //             lfp.post_id,
        //             COUNT(*) AS post_like_count
        //         FROM LatestFriendPostCTE lfp
        //         LEFT JOIN post_like pl ON lfp.post_id = pl.post_id
        //         WHERE lfp.rn = 1
        //         GROUP BY lfp.post_id
        //     ),
        //     CommentCountCTE AS (
        //         SELECT
        //             c.post_id,
        //         COUNT(c.comment_id) AS comment_count
        //         FROM comment c
        //         JOIN LatestFriendPostCTE lfp ON c.post_id = lfp.post_id
        //         WHERE lfp.rn = 1
        //             AND c.parent_id IS NULL
        //         GROUP BY c.post_id
        //     )
        //     SELECT
        //         f.friend_user_id,
        //         friend_user.username AS friend_username,
        //         friend_user.pfp_src AS friend_profile_picture,
        //         lfp.post_id,
        //         lfp.post_tile,
        //         lfp.post_content,
        //         lfp.post_has_img,
        //         lfp.post_media_src,
        //         lfp.post_created_at,
        //         COALESCE(plc.post_like_count, 0) AS post_like_count,
        //         COALESCE(cc.comment_count, 0) AS comment_count
        //     FROM FriendsCTE f
        //     JOIN [user] friend_user ON f.friend_user_id = friend_user.user_id
        //     LEFT JOIN LatestFriendPostCTE lfp ON f.friend_user_id = lfp.friend_user_id AND lfp.rn = 1 
        //     LEFT JOIN PostLikesCTE plc ON lfp.post_id = plc.post_id
        //     LEFT JOIN CommentCountCTE cc ON lfp.post_id = cc.post_id
        //     ORDER BY
        //         f.friend_user_id,
        //         lfp.post_created_at DESC;
        // ";

        public string get_first_10_posts_by_user_id() => @"
            WITH
                UserPostsCTE AS
                (
                    SELECT
                        p.post_id,
                        p.user_id,
                        p.content AS content,
                        p.edited AS edited,
                        p.edited_at AS edited_at,
                        p.created_at AS created_at,
                        ROW_NUMBER() OVER (ORDER BY p.created_at DESC) AS rn
                    FROM post p
                    WHERE p.user_id = @user_id
                ),
                UsernameCTE AS
                (
                    SELECT
                        u.username,
                        u.user_id,
                        u.pfp_src,
                        u.is_private
                    FROM [user] u
                    WHERE u.user_id = @user_id
                ),
                PostLikesCTE AS
                (
                    SELECT
                        pl.post_id,
                        COUNT(*) AS like_count
                    FROM post_like pl
                    GROUP BY pl.post_id
                ),
                CommentCountCTE AS
                (
                    SELECT
                        c.post_id,
                        COUNT(*) AS comment_count
                    FROM comment c
                    WHERE c.parent_id IS NULL
                    GROUP BY c.post_id
                ),
                MediaCTE AS
                (
                    SELECT
                        pm.post_id,
                        p.user_id,
                        (
                            SELECT
                                pm_inner.media_id,
                                pm_inner.media_type,
                                pm_inner.media_src,
                                pm_inner.[order]
                            FROM post_media pm_inner
                            WHERE pm_inner.post_id = pm.post_id
                            ORDER BY pm_inner.[order]
                            FOR JSON PATH
                        ) AS media_json
                    FROM post_media pm
                    INNER JOIN post p ON pm.post_id = p.post_id
                    GROUP BY pm.post_id, p.user_id
                ),
                RequesterLikesCTE AS
                (
                    SELECT
                        pl.post_id,
                        1 AS is_liked_requester
                    FROM post_like pl
                    WHERE pl.user_id = @req_user_id
                )

            SELECT
                up.post_id,
                up.user_id,
                COALESCE(u.username, '') AS username,
                COALESCE(u.pfp_src, '') AS pfp_src,
                COALESCE(u.is_private, '') AS is_private,
                up.content,
                up.edited,
                up.edited_at,
                up.created_at,
                COALESCE(plc.like_count, 0) AS like_count,
                COALESCE(cc.comment_count, 0) AS comment_count,
                COALESCE(m.media_json, '[]') AS media_json,

                CASE
                    WHEN up.user_id = @req_user_id THEN NULL
                    ELSE COALESCE(rl.is_liked_requester, 0)
                END AS is_liked_requester
            FROM UserPostsCTE up
                LEFT JOIN PostLikesCTE plc ON up.post_id = plc.post_id
                LEFT JOIN CommentCountCTE cc ON up.post_id = cc.post_id
                LEFT JOIN MediaCTE m ON up.post_id = m.post_id
                LEFT JOIN UsernameCTE u ON up.user_id = u.user_id
                LEFT JOIN RequesterLikesCTE rl ON up.post_id = rl.post_id
            WHERE up.rn <= 10
            ORDER BY up.created_at DESC;
        ";

        // public string update_post_by_id() => @"UPDATE [post] SET title = @title, content = @content WHERE post_id = @post_id;";
    }
}