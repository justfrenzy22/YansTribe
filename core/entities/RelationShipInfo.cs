using core.enums;

namespace core.entities
{
    public class RelationShipInfo
    {
        private bool _is_self;
        private bool _is_friend;
        private FriendShipStatus? _friendship_status;
        private string? _request_direction;

        public RelationShipInfo(bool is_self, bool is_friend, FriendShipStatus? friendship_status, string? request_direction)
        {
            this._is_self = is_self;
            this._is_friend = is_friend;
            this._friendship_status = friendship_status;
            this._request_direction = request_direction;
        }

        public bool is_self => this._is_self;
        public bool is_friend => this._is_friend;
        public FriendShipStatus? friendship_status => this._friendship_status;
        public string? request_direction => this._request_direction;
    }
}