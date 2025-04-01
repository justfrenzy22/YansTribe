namespace dal.queries
{
    public class UserQuery
    {
        public string add_user() => "INSERT INTO [user] (username, email, password_hash, full_name, bio, pfp_src, location, website) OUTPUT INSERTED.user_id VALUES (@username, @email, @password_hash, @full_name, @bio, @pfp_src, @location, @website)";
        public string get_user_by_id() => "SELECT * FROM [user] WHERE user_id = @user_id";
        // select * from [user] where username = 'testuser';

        public string get_user_by_username() => "select * from [user] where username = @username";

        public string get_user_by_email() => "SELECT * FROM [user] WHERE email = @email";
        public string get_user_by_email_and_password() => "SELECT * FROM [user] WHERE email = @email AND password_hash = @password_hash";

        public string get_last_insert_id() => "SELECT SCOPE_IDENTITY()";

        public string delete_user() => "DELETE FROM [user] WHERE user_id = @user_id";

        public string get_last_id() => "SELECT IDENT_CURRENT('user')";

        public string get_users() => "SELECT * FROM [user]";

        public string get_role_by_id() => "SELECT role FROM [user] WHERE user_id = @user_id";

        public string get_standard_users() => "SELECT * FROM [user] WHERE role = 'user'";

    }
}