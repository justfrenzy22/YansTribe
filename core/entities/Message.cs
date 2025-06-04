namespace core.entities
{
    public class Message
    {
        private Guid _message_id;
        private Guid _sender_id;
        private string _content;
        private DateTime _send_at;

        public Message(Guid message_id, Guid sender_id, string content, DateTime send_at)
        {
            this._message_id = message_id;
            this._sender_id = sender_id;
            this._content = content;
            this._send_at = send_at;
        }
        public Guid message_id => this._message_id;
        public Guid sender_id => this._sender_id;
        public string content => this._content;
        public DateTime send_at => this._send_at;
    }
}