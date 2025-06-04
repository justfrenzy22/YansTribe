namespace core.entities
{
    public class Chat
    {
        private Guid _chat_id;
        private UserAccount _user;
        private UserAccount _friend;
        private List<Message> _messages; // at least 10 messages
        private DateTime _created_at;
        public Chat(Guid chat_id, UserAccount user, UserAccount friend, DateTime created_at)
        {
            this._chat_id = chat_id;
            this._user = user;
            this._friend = friend;
            this._created_at = created_at;
            this._messages = new List<Message>();
        }
        // messages could be null
        public Guid chat_id => this._chat_id;
        public UserAccount user => this._user;
        public UserAccount friend => this._friend;
        public DateTime created_at => this._created_at;
        public List<Message> messages => this._messages;
        public void AddMessage(Message message) => this._messages.Add(message);
    }
}