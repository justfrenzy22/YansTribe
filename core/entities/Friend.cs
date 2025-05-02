using core.enums;

namespace core.entities
{
    public class Friend
    {
        private int _friendship_id;
        private int _user_1_id;
        private int _user_2_id;
        private FriendStatus _status;
        private DateTime _created_at;

        public Friend(int friendship_id, int user_1_id, int user_2_id, FriendStatus status, DateTime created_at)
        {
            this._friendship_id = friendship_id;
            this._user_1_id = user_1_id;
            this._user_2_id = user_2_id;
            this._status = status;
            this._created_at = created_at;
        }

        public int friendship_id => this._friendship_id;
        public int user_1_id => this._user_1_id;
        public int user_2_id => this._user_2_id;
        public FriendStatus status => this._status;
        public DateTime created_at => this._created_at;
    }


}