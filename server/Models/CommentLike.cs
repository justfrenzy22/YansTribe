namespace server.domains
{
    public class CommentLike
    {
        private int _user_id;
        private int _comment_id;
        private DateTime _created_at;

        public CommentLike(int user_id, int comment_id, DateTime created_at)
        {
            this._user_id = user_id;
            this._comment_id = comment_id;
            this._created_at = created_at;
        }

        public int user_id { get => this._user_id; }
        public int comment_id { get => this._comment_id; }
        public DateTime created_at { get => this._created_at; }

    }
}