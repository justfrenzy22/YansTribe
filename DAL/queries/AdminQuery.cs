namespace dal.queries
{
    public class AdminQuery
    {
        // public string get_admin_by_id() => "SELECT * FROM [admin] WHERE admin_id = @admin_id";
        public string get_admin_login() => "SELECT * FROM [user] WHERE email = @email AND password_hash = @password_hash";
    }
}