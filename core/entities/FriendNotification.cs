namespace core.entities
{
    public class FriendNotification
    {
        private UserAccount _sender;
        private DateTime _request_sent_at;

        public FriendNotification(UserAccount sender, DateTime request_sent_at)
        {
            this._sender = sender;
            this._request_sent_at = request_sent_at;
        }

        public UserAccount sender => this._sender;
        public DateTime request_sent_at => this._request_sent_at;
    }
}