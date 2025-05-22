using core.enums;

namespace core.entities
{
    public class SafeUser : BaseUser
    {
        private string _bio;
        private DateTime _created_at;
        private string _email;
        private string _full_name;
        private string _location;
        private Role _role;
        private string _website;

        public SafeUser(
            Guid user_id,
            string username,
            string pfp_src,
            string email,
            string full_name,
            string bio,
            string location,
            string website,
            bool is_private,
            DateTime created_at,
            Role role
        ) : base(user_id, username, pfp_src, is_private)
        {
            this._bio = bio;
            this._created_at = created_at;
            this._email = email;
            this._full_name = full_name;
            this._location = location;
            this._role = role;
            this._website = website;
        }

        public string bio => this._bio;
        public DateTime created_at => this._created_at;
        public string email => this._email;
        public string full_name => this._full_name;
        public string location => this._location;
        public Role role => this._role;
        public string website => this._website;
        public string RoleToString(Role role) => role.ToString();
    }
}
