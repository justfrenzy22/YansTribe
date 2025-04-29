namespace core.entities
{
    public class Post
    {
        private Guid _post_id;
        private Guid _user_id;

        private List<PostMedia> _media;
        // private string _media_src;
        private string _content;
        private DateTime _created_at;

        public Post(Guid post_id, Guid user_id, string content, DateTime created_at)
        {
            this._post_id = post_id;
            this._user_id = user_id;
            this._content = content;
            this._created_at = created_at;
        }

        public Post(Guid user_id, string content, DateTime created_at)
        {
            this._user_id = user_id;
            this._content = content;
            this._created_at = created_at;
        }

        public Guid post_id => this._post_id;
        public Guid user_id => this._user_id;
        public List<PostMedia> media => this._media;
        public string content => this._content;
        public DateTime created_at => this._created_at;

        public void AddMedia(PostMedia media)
        {
            if (this._media == null)
            {
                this._media = new List<PostMedia>();
            }
            this._media.Add(media);
        }


    }
}