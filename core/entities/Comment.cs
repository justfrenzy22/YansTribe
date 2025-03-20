namespace core.entities
{
    public class Comment
    {
        private int _comment_id;
        private int _post_id;
        private int _commenter_id;
        private int _parent_id;
        private string _content;
        private DateTime _created_at;

        public Comment(int comment_id, int post_id, int commenter_id, int parent_id, string content, DateTime created_at)
        {
            this._comment_id = comment_id;
            this._post_id = post_id;
            this._commenter_id = commenter_id;
            this._parent_id = parent_id;
            this._content = content;
            this._created_at = created_at;
        }

        public int comment_id => this._comment_id;
        public int post_id => this._post_id;
        public int commenter_id => this._commenter_id;
        public int parent_id => this._parent_id;
        public string content => this._content;
        public DateTime created_at => this._created_at;

    }
}