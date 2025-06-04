namespace core.entities
{
    public class UserAccount
    {
        private Guid _user_id;
        private string _username;
        private string _pfp_src;
        private bool _is_private;
        private Notifications _notifications;

        public UserAccount(Guid user_id, string username, string pfp_src, bool is_private)
        {
            this._user_id = user_id;
            this._username = username;
            this._pfp_src = pfp_src;
            this._is_private = is_private;
            this._notifications = new Notifications(null);
        }

        public void AddNotifications(Notifications notifications) => this._notifications = notifications;
        public Notifications notifications => this._notifications;
        public Guid user_id => this._user_id;
        public string username => this._username;
        public string pfp_src => this._pfp_src;
        public bool is_private => this._is_private;
    }
}