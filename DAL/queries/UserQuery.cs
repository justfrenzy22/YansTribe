namespace dal.queries
{
    public class UserQuery
    {
        public string add_user() => @"
            INSERT INTO [user] (username, email, password_hash, full_name, bio, location, website, role, is_private, created_at) OUTPUT INSERTED.user_id
            VALUES
                (@username, @email, @password_hash, @full_name, @bio, @location, @website, @role, @is_private, @created_at)";
        public string get_user_by_id() => @"
            SELECT * FROM [user]
            WHERE user_id = @user_id";

        public string get_user_profile_by_id() => @"
            WITH
                FriendshipCheck
                AS
                (
                    SELECT
                        f.status AS friendship_status,
                        f.user_1_id,
                        f.user_2_id
                    FROM friend f
                    WHERE 
                                (
                                    (f.user_1_id = @req_user_id AND f.user_2_id = @profile_user_id) OR
                        (f.user_1_id = @profile_user_id AND f.user_2_id = @req_user_id)
                                )
                ),
                FriendsCTE
                AS
                (
                    SELECT
                        CASE 
                                    WHEN f.user_1_id = @profile_user_id THEN f.user_2_id
                                    ELSE f.user_1_id
                                END AS friend_id,
                        f.status,
                        ROW_NUMBER() OVER (ORDER BY f.created_at DESC) AS rn
                    FROM friend f
                    WHERE 
                                (f.user_1_id = @profile_user_id OR f.user_2_id = @profile_user_id)
                        AND f.status = 'accepted'
                ),
                FriendDetails
                AS
                (
                    SELECT
                        f.friend_id AS user_id,
                        u.username,
                        u.pfp_src,
                        u.is_private,
                        f.status
                    FROM FriendsCTE f
                        JOIN [user] u ON u.user_id = f.friend_id
                    WHERE f.rn <= 8
                )

            SELECT
                u.user_id,
                u.username,
                u.pfp_src,
                u.is_private,

                -- Private fields
                CASE 
                                WHEN u.is_private = 0
                    OR @req_user_id = @profile_user_id
                    OR EXISTS (SELECT 1
                    FROM FriendshipCheck
                    WHERE friendship_status = 'accepted')
                                THEN u.full_name ELSE NULL 
                            END AS full_name,

                CASE 
                                WHEN u.is_private = 0
                    OR @req_user_id = @profile_user_id
                    OR EXISTS (SELECT 1
                    FROM FriendshipCheck
                    WHERE friendship_status = 'accepted')
                                THEN u.email ELSE NULL 
                            END AS email,

                CASE 
                                WHEN u.is_private = 0
                    OR @req_user_id = @profile_user_id
                    OR EXISTS (SELECT 1
                    FROM FriendshipCheck
                    WHERE friendship_status = 'accepted')
                                THEN u.bio ELSE NULL 
                            END AS bio,

                CASE 
                                WHEN u.is_private = 0
                    OR @req_user_id = @profile_user_id
                    OR EXISTS (SELECT 1
                    FROM FriendshipCheck
                    WHERE friendship_status = 'accepted')
                                THEN u.[location] ELSE NULL 
                            END AS [location],

                CASE 
                                WHEN u.is_private = 0
                    OR @req_user_id = @profile_user_id
                    OR EXISTS (SELECT 1
                    FROM FriendshipCheck
                    WHERE friendship_status = 'accepted')
                                THEN u.website ELSE NULL 
                            END AS website,

                CASE 
                                WHEN u.is_private = 0
                    OR @req_user_id = @profile_user_id
                    OR EXISTS (SELECT 1
                    FROM FriendshipCheck
                    WHERE friendship_status = 'accepted')
                                THEN u.role ELSE NULL 
                            END AS role,

                u.created_at,

                (
                                SELECT COUNT(*)
                FROM friend f
                WHERE (f.user_1_id = @profile_user_id OR f.user_2_id = @profile_user_id)
                    AND f.status = 'accepted'
                            ) AS friends_num,

                (
                                SELECT
                    user_id,
                    username,
                    pfp_src,
                    is_private,
                    status
                FROM FriendDetails
                FOR JSON PATH
                            ) AS friends_json,

                -- ✅ Is the requesting user friends with the profile user?
                CASE 
                                WHEN EXISTS (
                                    SELECT 1
                FROM FriendshipCheck
                WHERE friendship_status = 'accepted'
                                ) THEN 1 ELSE 0
                            END AS is_friend,

                -- ✅ Friendship status (if any)
                (
                                SELECT TOP 1
                    friendship_status
                FROM FriendshipCheck
                            ) AS friendship_status,

                -- ✅ Who sent the request? ('sent', 'received', or NULL)
                (
                                SELECT
                    CASE 
                                        WHEN f.friendship_status = 'pending' THEN
                                            CASE 
                                                WHEN f.user_1_id = @req_user_id THEN 'sent'
                                                WHEN f.user_1_id = @profile_user_id THEN 'received'
                                                ELSE NULL
                                            END
                                        ELSE NULL
                                    END
                FROM FriendshipCheck f
                            ) AS request_direction,

                -- ✅ Is the requesting user the profile owner?
                CASE 
                                WHEN @req_user_id = @profile_user_id THEN 1
                                ELSE 0
                            END AS is_self

            FROM [user] u
            WHERE u.user_id = @profile_user_id;
        ";

        public string get_friend_notifications() => @"
            SELECT 
                f.user_1_id AS sender_id,
                u.username AS username,
                u.pfp_src AS pfp_src,
                u.is_private AS is_private,
                f.created_at AS request_sent_at
            FROM friend f
            JOIN [user] u ON u.user_id = f.user_1_id
            WHERE 
                f.user_2_id = @user_id
                AND f.status = 'pending';
        ";

        public string get_user_essentials_by_id() => @"
            SELECT user_id, username, pfp_src, is_private
            FROM [user]
            WHERE user_id = @user_id";

        public string get_user_by_username() => @"
        SELECT * FROM [user]
        WHERE username = @username";

        public string get_user_by_email() => "SELECT * FROM [user] WHERE email = @email";
        public string check_user_by_username() => "SELECT user_id FROM [user] WHERE username = @username";
        public string get_user_by_email_and_password() => @"
            SELECT * FROM [user]
            WHERE email = @email AND password_hash = @password_hash";

        public string get_last_insert_id() => "SELECT SCOPE_IDENTITY()";

        public string delete_user() => @"
            DELETE FROM [user]
            WHERE user_id = @user_id";

        public string get_last_id() => "SELECT IDENT_CURRENT('user')";

        public string get_users() => "SELECT * FROM [user]";

        public string get_role_by_id() => @"
            SELECT role
            FROM [user]
            WHERE user_id = @user_id";

        public string get_standard_users() => "SELECT * FROM [user] WHERE role = 'user'";

        public string update_user_role() => @"
            UPDATE [user]
            SET role = @role
            WHERE user_id = @user_id";

    }
}