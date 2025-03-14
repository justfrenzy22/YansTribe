namespace server.models
{
    public class Message
    {
        private int _message_id;
        private int _chat_id;
        private int _sender_id;
        private string _content;
        private DateTime _send_at;

        public Message(int message_id, int chat_id, int sender_id, string content, DateTime send_at)
        {
            this._message_id = message_id;
            this._chat_id = chat_id;
            this._sender_id = sender_id;
            this._content = content;
            this._send_at = send_at;
        }


        public int message_id { get => this._message_id; }
        public int chat_id { get => this._chat_id; }
        public int sender_id { get => this._sender_id; }
        public string content { get => this._content; }
        public DateTime send_at { get => this._send_at; }
    }
}