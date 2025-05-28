namespace core.entities
{
    public class CommentLike
    {
        private Guid _user_id;
        private Guid _comment_id;
        private DateTime _created_at;

        public CommentLike(Guid user_id, Guid comment_id, DateTime created_at)
        {
            this._user_id = user_id;
            this._comment_id = comment_id;
            this._created_at = created_at;
        }

        public Guid user_id => this._user_id;
        public Guid comment_id => this._comment_id;
        public DateTime created_at => this._created_at;

    }
}