
using core.enums;

namespace core.entities
{
    public class Story
    {
        private int _story_id;
        private int _user_id;
        private MediaType _media_type;
        private string _media_src;
        private DateTime _created_at;
        private DateTime _expires_at;

        public Story(int story_id, int user_id, MediaType media_type, string media_src, DateTime created_at, DateTime expires_at)
        {
            this._story_id = story_id;
            this._user_id = user_id;
            this._media_type = media_type;
            this._media_src = media_src;
            this._created_at = created_at;
            this._expires_at = expires_at;
        }

        public int story_id => this._story_id;
        public int user_id => this._user_id;
        public MediaType media_type => this._media_type;
        public string media_src => this._media_src;
        public DateTime created_at => this._created_at;
        public DateTime expires_at => this._expires_at;
    }
}