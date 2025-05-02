using core.enums;

namespace core.entities
{
    public class PostMedia
    {
        private Guid _post_id;
        private Guid _media_id;
        private MediaType _media_type;
        private string _media_src;

        public PostMedia(Guid post_id, Guid media_id, MediaType media_type, string media_src)
        {
            this._post_id = post_id;
            this._media_id = media_id;
            this._media_type = media_type;
            this._media_src = media_src;
        }

        public Guid media_id => this._media_id;
        public Guid post_id => this._post_id;
        public MediaType media_type => this._media_type;
        public string media_src => this._media_src;
    }
}