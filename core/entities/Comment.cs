namespace core.entities
{
    public class Comment
    {
        private Guid _comment_id;
        private Guid _post_id;
        private Guid _commenter_id;
        private Guid _parent_id;
        private string _content;
        private DateTime _created_at;


        public Comment(Guid comment_id, Guid post_id, Guid commenter_id, Guid parent_id, string content, DateTime created_at)
        {
            this._comment_id = comment_id;
            this._post_id = post_id;
            this._commenter_id = commenter_id;
            this._parent_id = parent_id;
            this._content = content;
            this._created_at = created_at;
        }

        public Guid comment_id => this._comment_id;
        public Guid post_id => this._post_id;
        public Guid commenter_id => this._commenter_id;
        public Guid parent_id => this._parent_id;
        public string content => this._content;
        public DateTime created_at => this._created_at;

    }
}