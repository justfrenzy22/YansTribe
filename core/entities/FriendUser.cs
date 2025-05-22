using core.enums;

namespace core.entities
{
    public class FriendUser : BaseUser
    {
        private FriendStatus _status;

        public FriendUser(FriendStatus status, Guid user_id, string username, string pfp_src, bool is_private) : base(user_id, username, pfp_src, is_private)
        {
            this._status = status;
        }
        public FriendStatus status => this._status;
    }
}