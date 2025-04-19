using core.enums;

namespace core.entities
{
    public class User
    {
        private Guid _user_id;
        private string _username;
        private string _email;
        private string _password;
        private string _full_name;
        private string _bio;
        private string? _pfp_src;
        private string _location;
        private string _website;
        private bool _is_private;
        private DateTime _created_at;
        private Role _role;

        public User(Guid user_id, string username, string email, string password, string full_name, string bio, string pfp_src, string location, string website, bool is_private, DateTime created_at, Role role)
        {
            this._user_id = user_id;
            this._username = username;
            this._email = email;
            this._password = password;
            this._full_name = full_name;
            this._bio = bio;
            this._pfp_src = pfp_src;
            this._location = location;
            this._website = website;
            this._is_private = is_private;
            this._created_at = created_at;
            this._role = role;
        }

        public User(Guid user_id, string username, string email, string full_name, string bio, string pfp_src, string location, string website, bool is_private, DateTime created_at, Role role)
        {
            this._user_id = user_id;
            this._username = username;
            this._email = email;
            this._full_name = full_name;
            this._bio = bio;
            this._pfp_src = pfp_src;
            this._location = location;
            this._website = website;
            this._is_private = is_private;
            this._created_at = created_at;
            this._role = role;
        }

        public User(string username, string email, string password, string full_name, string bio, string location, string website, Role role, bool is_private, DateTime created_at)
        {
            this._username = username;
            this._email = email;
            this._password = password;
            this._full_name = full_name;
            this._bio = bio;
            this._location = location;
            this._website = website;
            this._role = role;
            this._is_private = is_private;
            this._created_at = created_at;
        }

        // Mock data constructor
        public User(Guid user_id, string email, string password, Role role)
        {
            this._user_id = user_id;
            this._email = email;
            this._password = password;
            this._role = role;
        }

        public Guid user_id => this._user_id;
        public string username => this._username;
        public string email => this._email;
        public string password => this._password;
        public string full_name => this._full_name;
        public string bio => this._bio;
        public string pfp_src => this._pfp_src ?? string.Empty;
        public string location => this._location;
        public string website => this._website;
        public bool is_private => this._is_private;
        public DateTime created_at => this._created_at;
        public Role role => this._role;

        // public Role ParseRole<Role>(string value) => (Role)Enum.Parse(typeof(Role), value, true);




        public void HashPassword(string hash_password) => this._password = hash_password;
    }

}