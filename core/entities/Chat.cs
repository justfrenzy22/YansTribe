namespace core.entities
{
    public class Chat
    {
        private Guid _chat_id;
        private Guid _user_1_id;
        private Guid _user_2_id;
        private DateTime _created_at;

        public Chat(Guid chat_id, Guid user_1_id, Guid user_2_id, DateTime created_at)
        {
            this._chat_id = chat_id;
            this._user_1_id = user_1_id;
            this._user_2_id = user_2_id;
            this._created_at = created_at;
        }

        public Guid chat_id => _chat_id;
        public Guid user_1_id => _user_1_id;
        public Guid user_2_id => _user_2_id;
        public DateTime created_at => _created_at;
    }
}