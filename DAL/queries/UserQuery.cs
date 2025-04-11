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

        public string get_user_by_username() => @"
        SELECT * FROM [user]
        WHERE username = @username";

        public string get_user_by_email() => "SELECT * FROM [user] WHERE email = @email";
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