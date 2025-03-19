namespace core.entities
{
    public class PostLike
    {
        private int _user_id;
        private int _post_id;
        private DateTime _created_at;

        public PostLike(int user_id, int post_id, int issuer_id, DateTime created_at)
        {
            this._user_id = user_id;
            this._post_id = post_id;
            this._created_at = created_at;
        }

        public int user_id { get => this._user_id; }
        public int post_id { get => this._post_id; }
        public DateTime created_at { get => this._created_at; }
    }
}