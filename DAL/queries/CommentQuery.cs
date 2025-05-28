namespace dal.queries
{
    public class CommentQuery
    {
        // public string get_init_comments_by_post_id() => @"SELECT TOP 10 * FROM [comment] WHERE post_id = @post_id ORDER BY created_at DESC;";
        public string get_init_comments_by_post_id() => @"
            SELECT TOP 10
                c.comment_id,
                c.post_id,
                c.content,
                c.created_at,
                u.username,
                u.pfp_src
            FROM comment c
            JOIN [user] u ON c.user_id = u.user_id
            WHERE c.parent_id IS NULL
                AND c.post_id = @post_id
            ORDER BY c.created_at ASC;
        ";

        public string add_comment () => @"
            INSERT INTO comment (post_id, user_id, parent_id, content, created_at) VALUES
            (
                (SELECT post_id FROM post WHERE post_id = @post_id),
                (SELECT user_id FROM [user] WHERE user_id = @user_id),
                @parent_id,
                @content,
                @created_at
            )
        ";

        public string add_comment_like () => @"
            INSERT INTO comment_like (comment_id, user_id, created_at) VALUES
            (
                (SELECT comment_id FROM comment WHERE comment_id = @comment_id),
                (SELECT user_id FROM [user] WHERE user_id = @user_id),
                @created_at
            )
        ";

        public string delete_comment_like () => @"
            DELETE FROM comment_like
            WHERE comment_id = @comment_id
                AND user_id = @user_id;
        ";

        public string delete_comment_by_id () => @"
            DELETE FROM comment
            WHERE comment_id = @comment_id;
        ";



    }
}