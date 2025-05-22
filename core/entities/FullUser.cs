using core.enums;

namespace core.entities
{
    public class FullUser : SafeUser
    {
        private string _password;
        public FullUser(Guid user_id, string username, string pfp_src, string email, string full_name, string bio, string location, string website, bool is_private, DateTime created_at, Role role, string password) : base(user_id, username, pfp_src, email, full_name, bio, location, website, is_private, created_at, role)
        {
            this._password = password;
        }
        public string password => this._password;
        public void HashPassword(string hash_password) => this._password = hash_password;
    }
}