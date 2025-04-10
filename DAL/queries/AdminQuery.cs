namespace dal.queries
{
    public class AdminQuery
    {
        // public string get_admin_by_id() => "SELECT * FROM [admin] WHERE admin_id = @admin_id";
        public string get_admin_login() => "SELECT * FROM [user] WHERE email = @email AND password_hash = @password_hash";
        public string get_all_users() => @"
            IF EXISTS (SELECT 1 FROM [user] WHERE user_id = @admin_id AND [role] = 'superadmin')
            BEGIN
                SELECT * FROM [user];
            END
            ELSE IF EXISTS (SELECT 1 FROM [user] WHERE user_id = @admin_id AND [role] = 'admin')
            BEGIN
                SELECT * FROM [user] WHERE [role] = 'user';
            END
            ELSE
            BEGIN
                SELECT * FROM [user] WHERE 1 = 0;
            END;
        ";
    }
}