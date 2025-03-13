using server.enums;

namespace server.models
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


        public int user_id { get => this._user_id; }
        public string username { get => this._username; }
        public string email { get => this._email; }
        public string password_hash { get => this._password_hash; }
        public string full_name { get => this._full_name; }
        public string bio { get => this._bio; }
        public string pfp_src { get => this._pfp_src; }
        public string location { get => this._location; }
        public string website { get => this._website; }
        public bool is_private { get => this._is_private; }
        public DateTime created_at { get => this._created_at; }
        public Role role { get => this._role; }

        public static Role ParseRole<Role>(string value)
        {
            return (Role)Enum.Parse(typeof(Role), value, true);
        }
    }

}