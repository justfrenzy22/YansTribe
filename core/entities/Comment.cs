namespace core.entities
{
    public class Comment
    {
        private Guid _comment_id;
        private Guid _post_id;
        private BaseUser _user;
        private Guid _parent_id;
        private string _content;
        private DateTime _created_at;

        private int reply_count;
        private int like_count;

        private bool is_liked_requester;


        public Comment(Guid comment_id, Guid post_id, BaseUser user, Guid parent_id, string content, DateTime created_at, int reply_count, int like_count, bool is_liked_requester)
        {
            this._comment_id = comment_id;
            this._post_id = post_id;
            this._user = user;
            this._parent_id = parent_id;
            this._content = content;
            this._created_at = created_at;
            this.reply_count = reply_count;
            this.like_count = like_count;
            this.is_liked_requester = is_liked_requester;
        }

        public Guid comment_id => this._comment_id;
        public Guid post_id => this._post_id;
        public BaseUser user => this._user;
        public Guid parent_id => this._parent_id;
        public string content => this._content;
        public DateTime created_at => this._created_at;

    }
}