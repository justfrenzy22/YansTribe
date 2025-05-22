namespace core.entities
{
    public class BasePost
    {
        protected Guid _post_id;
        protected List<PostMedia> _media;
        protected string _content;
        protected DateTime _created_at;

        public BasePost(Guid post_id, string content, DateTime created_at)
        {
            _post_id = post_id;
            _content = content;
            _created_at = created_at;
            _media = new List<PostMedia>();
        }

        public Guid post_id => _post_id;
        public List<PostMedia> media => _media;
        public string content => _content;
        public DateTime created_at => _created_at;

        public void AddMedia(PostMedia media)
        {
            _media.Add(media);
        }
    }
}
