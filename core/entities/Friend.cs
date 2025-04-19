using core.enums;

namespace core.entities
{
    public class Friend
    {
        private Guid _friendship_id;
        private Guid _user_1_id;
        private Guid _user_2_id;
        private FriendStatus _status;
        private DateTime _created_at;

        public Friend(Guid friendship_id, Guid user_1_id, Guid user_2_id, FriendStatus status, DateTime created_at)
        {
            this._friendship_id = friendship_id;
            this._user_1_id = user_1_id;
            this._user_2_id = user_2_id;
            this._status = status;
            this._created_at = created_at;
        }

        public Guid friendship_id => this._friendship_id;
        public Guid user_1_id => this._user_1_id;
        public Guid user_2_id => this._user_2_id;
        public FriendStatus status => this._status;
        public DateTime created_at => this._created_at;
    }


}