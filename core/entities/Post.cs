namespace core.entities
{
    public class Post : BasePost
    {
        private bool _edited;
        private DateTime _edited_at;
        private int _like_count;
        private int _comment_count;
        private UserAccount _user;
        private bool _is_liked_requester;

        public Post(Guid post_id, string content, bool edited, DateTime edited_at, DateTime created_at, int like_count, int comment_count, bool is_liked_requester, UserAccount user) : base(post_id, content, created_at)
        {
            this._edited = edited;
            this._edited_at = edited_at;
            this._like_count = like_count;
            this._comment_count = comment_count;
            this._is_liked_requester = is_liked_requester;
            this._user = user;
        }

        public DateTime edited_at => this._edited_at;
        public bool edited => this._edited;
        public int like_count => this._like_count;
        public int comment_count => this._comment_count;
        public bool is_liked_requester => this._is_liked_requester;
        public UserAccount user => this._user;

    }
}