namespace core.entities
{
    public class Comment
    {
        private Guid _comment_id;
        private Guid _post_id;
        private UserAccount _user;
        private Guid _parent_id;
        private string _content;
        private DateTime _created_at;

        private int _reply_count;
        private int _like_count;

        private bool _is_liked_requester;


        public Comment(Guid comment_id, Guid post_id, UserAccount user, Guid parent_id, string content, DateTime created_at, int reply_count, int like_count, bool is_liked_requester)
        {
            this._comment_id = comment_id;
            this._post_id = post_id;
            this._user = user;
            this._parent_id = parent_id;
            this._content = content;
            this._created_at = created_at;
            this._reply_count = reply_count;
            this._like_count = like_count;
            this._is_liked_requester = is_liked_requester;
        }

        public Guid comment_id => this._comment_id;
        public Guid post_id => this._post_id;
        public UserAccount user => this._user;
        public Guid parent_id => this._parent_id;
        public string content => this._content;
        public DateTime created_at => this._created_at;
    }
}