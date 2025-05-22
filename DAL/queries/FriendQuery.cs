namespace dal.queries
{
    public class FriendQuery
    {
        public string add_friend() => @"
        IF NOT EXISTS (
            SELECT 1
            FROM friend f
            WHERE
                (
                    (f.user_1_id = @req_user_id AND f.user_2_id = @user2) OR
                    (f.user_1_id = @user2 AND f.user_2_id = @req_user_id)
                )
                AND f.status IN ('accepted', 'pending', 'blocked')
        )
        BEGIN
            INSERT INTO friend (user_1_id, user_2_id, status, created_at)
            VALUES (@req_user_id, @user2, 'pending', GETDATE());
        END
        ELSE
        BEGIN
            PRINT 'Friendship already exists or is blocked.';
        END
        ";

        public string get_friendship_status() => @"
            SELECT *
            FROM friend
            WHERE user_1_id = @req_user_id
            AND user_2_id = @friend_id
            OR user_1_id = @friend_id
            AND user_2_id = @req_user_id";

        public string remove_friend() => @"
            DELETE friend
            WHERE
                (user_1_id = @req_user_id AND user_2_id = @user2)
                OR (user_1_id = @user2 AND user_2_id = @req_user_id)
        ";

        public string get_friends_by_user_id() => "SELECT * FROM [friend] WHERE user_id = @user_id";

        public string cancel_friend() => @"
            DELETE friend
            WHERE
                (user_1_id = @req_user_id AND user_2_id = @user2)
                OR (user_1_id = @user2 AND user_2_id = @req_user_id)
                AND status = 'pending'
        ";

        public string accept_friend() => @"
            UPDATE friend
            SET status = 'accepted'
            WHERE friendship_id = @friendship_id
        ";

        public string reject_friend() => @"
            DELETE friend
            WHERE friendship_id = @friendship_id
        ";
    }
}