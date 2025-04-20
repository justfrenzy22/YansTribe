namespace dal.queries
{
    public class PostQuery
    {
        public string get_post_by_id() => @"SELECT * FROM [post] WHERE post_id = @post_id;";
        public string get_first_10_posts_by_user_id() => @"
        SELECT TOP 10 * FROM [post]
        WHERE user_id = @user_id
        ORDER BY created_at DESC;";

        public string add_post() => @"
        DECLARE @InsertedPostId UNIQUEIDENTIFIER;

        INSERT INTO post (user_id, content, created_at)
        OUTPUT INSERTED.post_id INTO @InsertedPostId
        VALUES (@user_id, @content, @created_at);

        SELECT @InsertedPostId;
        ";

        public string delete_post_by_id() => @"DELETE FROM [post] WHERE post_id = @post_id;";

        public string add_post_like() => @"
            INSERT INTO post_like (post_id, user_id, created_at) VALUES
            (
                (SELECT post_id FROM post WHERE post_id = @post_id),
                (SELECT user_id FROM [user] WHERE user_id = @user_id),
                @created_at
            );
        ";

        public string add_post_media() => @"
        INSERT INTO post_media (post_id, media_type, media_src)
        VALUES (@post_id, @media_type, @media_src);";

        public string delete_post_like() => @"
        DELETE FROM post_like
        WHERE post_id = @post_id
            AND user_id = @user_id;";


        public string get_first_10_private_posts() => @"
            WITH FriendsCTE AS (
                SELECT
                    CASE
                        WHEN f.user_1_id = @user_id THEN f.user_2_id
                        ELSE f.user_1_id
                    END AS friend_user_id
                FROM friend f
                WHERE (f.user_1_id = 1 OR f.user_2_id = @user_id)
                    AND f.status = 'accepted'
            ),
            LatestFriendPostCTE AS (
                SELECT
                    p.post_id,
                    p.title as post_tile,
                    p.user_id AS friend_user_id,
                    p.content AS post_content,
                    p.has_img as post_has_img,
                    p.media_src as post_media_src,
                    p.created_at AS post_created_at,
                    ROW_NUMBER() OVER(PARTITION BY p.user_id ORDER BY p.created_at DESC) as rn
                FROM post p
                JOIN FriendsCTE f ON p.user_id = f.friend_user_id
            ),
            PostLikesCTE AS (
                SELECT
                    lfp.post_id,
                    COUNT(*) AS post_like_count
                FROM LatestFriendPostCTE lfp
                LEFT JOIN post_like pl ON lfp.post_id = pl.post_id
                WHERE lfp.rn = 1
                GROUP BY lfp.post_id
            ),
            CommentCountCTE AS (
                SELECT
                    c.post_id,
                COUNT(c.comment_id) AS comment_count
                FROM comment c
                JOIN LatestFriendPostCTE lfp ON c.post_id = lfp.post_id
                WHERE lfp.rn = 1
                    AND c.parent_id IS NULL
                GROUP BY c.post_id
            )
            SELECT
                f.friend_user_id,
                friend_user.username AS friend_username,
                friend_user.pfp_src AS friend_profile_picture,
                lfp.post_id,
                lfp.post_tile,
                lfp.post_content,
                lfp.post_has_img,
                lfp.post_media_src,
                lfp.post_created_at,
                COALESCE(plc.post_like_count, 0) AS post_like_count,
                COALESCE(cc.comment_count, 0) AS comment_count
            FROM FriendsCTE f
            JOIN [user] friend_user ON f.friend_user_id = friend_user.user_id
            LEFT JOIN LatestFriendPostCTE lfp ON f.friend_user_id = lfp.friend_user_id AND lfp.rn = 1 
            LEFT JOIN PostLikesCTE plc ON lfp.post_id = plc.post_id
            LEFT JOIN CommentCountCTE cc ON lfp.post_id = cc.post_id
            ORDER BY
                f.friend_user_id,
                lfp.post_created_at DESC;
        ";



        // public string update_post_by_id() => @"UPDATE [post] SET title = @title, content = @content WHERE post_id = @post_id;";
    }
}