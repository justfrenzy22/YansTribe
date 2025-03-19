namespace dal.queries
{
    public class AdminQuery
    {
        // public string get_admin_by_id() => "SELECT * FROM [admin] WHERE admin_id = @admin_id";
        public string get_admin_login() => "SELECT * FROM [admin] WHERE email = @username AND password_hash = @password_hash AND role = 'admin'";
    }
}