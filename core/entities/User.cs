using core.enums;

namespace core.entities
{
    public class User
    {
        private int _user_id;
        private string _username;
        private string _email;
        private string _password_hash;
        private string _full_name;
        private string _bio;
        private string _pfp_src;
        private string _location;
        private string _website;
        private bool _is_private;
        private DateTime _created_at;
        private Role _role;

        public User(int user_id, string username, string email, string password_hash, string full_name, string bio, string pfp_src, string location, string website, bool is_private, DateTime created_at, Role role)
        {
            this._user_id = user_id;
            this._username = username;
            this._email = email;
            this._password_hash = password_hash;
            this._full_name = full_name;
            this._bio = bio;
            this._pfp_src = pfp_src;
            this._location = location;
            this._website = website;
            this._is_private = is_private;
            this._created_at = created_at;
            this._role = role;
        }

        public int user_id => this._user_id;
        public string username => this._username;
        public string email => this._email;
        public string password_hash => this._password_hash;
        public string full_name => this._full_name;
        public string bio => this._bio;
        public string pfp_src => this._pfp_src;
        public string location => this._location;
        public string website => this._website;
        public bool is_private => this._is_private;
        public DateTime created_at => this._created_at;
        public Role role => this._role;

        public Role ParseRole<Role>(string value) => (Role)Enum.Parse(typeof(Role), value, true);
    }

}