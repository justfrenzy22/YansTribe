using core.enums;

namespace core.entities
{
    public class Friendship : UserAccount
    {
        private FriendShipStatus _status;

        public Friendship(FriendShipStatus status, Guid user_id, string username, string pfp_src, bool is_private) : base(user_id, username, pfp_src, is_private)
        {
            this._status = status;
        }
        public FriendShipStatus status => this._status;
    }
}