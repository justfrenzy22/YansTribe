namespace dal.queries
{
    public class CommentQuery
    {
        // public string get_init_comments_by_post_id() => @"SELECT TOP 10 * FROM [comment] WHERE post_id = @post_id ORDER BY created_at DESC;";
        public string get_init_comments() => @"
            DECLARE @target_post_id UNIQUEIDENTIFIER = @post_id;
            DECLARE @req_user_id UNIQUEIDENTIFIER =  @user_id;

            WITH BaseComments AS (
                SELECT TOP 10 *
                FROM comment
                WHERE post_id = @target_post_id AND parent_id IS NULL
                ORDER BY created_at ASC
            ),

            ReplyCounts AS (
                SELECT parent_id, COUNT(*) AS reply_count
                FROM comment
                WHERE parent_id IS NOT NULL AND post_id = @target_post_id
                GROUP BY parent_id
            ),

            LikeCounts AS (
                SELECT comment_id, COUNT(*) AS like_count
                FROM comment_like
                GROUP BY comment_id
            ),

            RequesterLikes AS (
                SELECT comment_id, 1 AS is_liked_requester
                FROM comment_like
                WHERE user_id = @req_user_id
            ),

            CommentUser AS (
                SELECT user_id, username, pfp_src, is_private
                FROM [user]
            )

            SELECT
                bc.post_id,
                bc.comment_id,
                bc.user_id,
                bc.parent_id,
                ISNULL(u.username, '') AS username,
                ISNULL(u.pfp_src, '') AS pfp_src,
                ISNULL(u.is_private, 0) AS is_private,
                bc.content,
                bc.created_at,
                bc.edited,
                bc.edited_at,
                ISNULL(rc.reply_count, 0) AS reply_count,
                ISNULL(lc.like_count, 0) AS like_count,
                ISNULL(rl.is_liked_requester, 0) AS is_liked_requester
            FROM BaseComments bc
            LEFT JOIN ReplyCounts rc ON rc.parent_id = bc.comment_id
            LEFT JOIN LikeCounts lc ON lc.comment_id = bc.comment_id
            LEFT JOIN RequesterLikes rl ON rl.comment_id = bc.comment_id
            LEFT JOIN CommentUser u ON u.user_id = bc.user_id
            ORDER BY bc.created_at DESC;
        ";

        public string add_comment() => @"
            INSERT INTO comment (post_id, user_id, content, created_at)
            VALUES
            (@post_id, @user_id, @content, @created_at)";

        public string add_comment_like() => @"
            INSERT INTO comment_like (comment_id, user_id, created_at) VALUES
            (
                (SELECT comment_id FROM comment WHERE comment_id = @comment_id),
                (SELECT user_id FROM [user] WHERE user_id = @user_id),
                @created_at
            )
        ";

        public string delete_comment_like() => @"
            DELETE FROM comment_like
            WHERE comment_id = @comment_id
                AND user_id = @user_id;
        ";

        public string delete_comment_by_id() => @"
            DELETE FROM comment
            WHERE comment_id = @comment_id;
        ";



    }
}