namespace core.entities
{
    public class FriendNotification
    {
        private Guid _sender_id;
        private string _username;
        private string _pfp_src;
        private DateTime _request_sent_at;

        public FriendNotification(Guid sender_id, string username, string pfp_src, DateTime request_sent_at)
        {
            this._sender_id = sender_id;
            this._username = username;
            this._pfp_src = pfp_src;
            this._request_sent_at = request_sent_at;
        }
        public Guid sender_id => this._sender_id;
        public string username => this._username;
        public string pfp_src => this._pfp_src;
        public DateTime request_sent_at => this._request_sent_at;
    }
}