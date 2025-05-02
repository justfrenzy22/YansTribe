namespace core.entities
{
    public class Chat
    {
        private int _chat_id;
        private int _user_1_id;
        private int _user_2_id;
        private DateTime _created_at;

        public Chat(int chat_id, int user_1_id, int user_2_id, DateTime created_at)
        {
            this._chat_id = chat_id;
            this._user_1_id = user_1_id;
            this._user_2_id = user_2_id;
            this._created_at = created_at;
        }

        public int chat_id => _chat_id;
        public int user_1_id => _user_1_id;
        public int user_2_id => _user_2_id;
        public DateTime created_at => _created_at;
    }
}