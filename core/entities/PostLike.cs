namespace core.entities
{
    public class PostLike
    {
        private Guid _user_id;
        private Guid _post_id;
        private DateTime _created_at;

        public PostLike(Guid user_id, Guid post_id, int issuer_id, DateTime created_at)
        {
            this._user_id = user_id;
            this._post_id = post_id;
            this._created_at = created_at;
        }

        public Guid user_id => this._user_id;
        public Guid post_id => this._post_id;
        public DateTime created_at => this._created_at;
    }
}