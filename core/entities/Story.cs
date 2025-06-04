using core.enums;

namespace core.entities
{
    public class Story
    {
        private Guid _story_id;
        private UserAccount _user;
        private MediaType _media_type;
        private string _media_src;
        private string _caption;
        private DateTime _created_at;
        private DateTime _expires_at;

        public Story(Guid story_id, UserAccount user, MediaType media_type, string media_src, string caption, DateTime created_at, DateTime expires_at)
        {
            this._story_id = story_id;
            this._user = user;
            this._media_type = media_type;
            this._media_src = media_src;
            this._caption = caption;
            this._created_at = created_at;
            this._expires_at = expires_at;
        }

        public Guid story_id => this._story_id;
        public UserAccount user => this._user;
        public MediaType media_type => this._media_type;
        public string media_src => this._media_src;
        public string caption => this._caption;
        public DateTime created_at => this._created_at;
        public DateTime expires_at => this._expires_at;
    }
}