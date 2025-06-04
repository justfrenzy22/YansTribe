namespace core.entities
{
    public class PostCreateEntity : BasePost
    {
        private Guid _user_id;

        public PostCreateEntity(Guid post_id, Guid user_id, string content, DateTime created_at) : base(post_id, content, created_at) =>
            this._user_id = user_id;

        public Guid user_id => this._user_id;
    }
}