using bll.interfaces;
using core.entities;
using dal.interfaces.repo;

namespace bll.services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepo _friend_repo;

        public FriendService(IFriendRepo friend_repo)
        {
            this._friend_repo = friend_repo;
        }


        public async Task<string?> AcceptFriend(Guid req_user_id, Guid user2_id)
        {

            Friend? friend = await this._friend_repo.GetFriendshipStatus(req_user_id, user2_id);

            if (friend == null)
            {
                return null;
            }

            if (friend.status == core.enums.FriendStatus.pending)
            {
                await this._friend_repo.AcceptFriend(friendship_id: friend.friendship_id);
            }

            return "Friendship accepted.";
        }

        public async Task<string?> DeclineFriend(Guid req_user_id, Guid user2_id)
        {
            Friend? friend = await this._friend_repo.GetFriendshipStatus(req_user_id, user2_id);

            if (friend == null)
            {
                return null;
            }

            if (friend.status == core.enums.FriendStatus.pending)
            {
                await this._friend_repo.RejectFriend(friendship_id: friend.friendship_id);
            }

            return "Friendship declined.";
        }

        public async Task AddFriend(Guid req_user_id, Guid user2_id) => await this._friend_repo.AddFriend(req_user_id, user2_id);

        public async Task<string?> RemoveFriend(Guid req_user_id, Guid user2_id)
        {
            Friend? friend = await this._friend_repo.GetFriendshipStatus(req_user_id, user2_id);

            if (friend == null)
            {
                return null;
            }

            if (friend.status == core.enums.FriendStatus.accepted)
            {
                await this._friend_repo.RemoveFriend(req_user_id, user2_id);
            }

            return "Friend removed.";
        }

        public async Task<string?> CancelFriend(Guid req_user_id, Guid user2_id)
        {
            Friend? friend = await this._friend_repo.GetFriendshipStatus(req_user_id, user2_id);

            if (friend == null)
            {
                return null;
            }

            if (friend.status == core.enums.FriendStatus.accepted)
            {
                await this._friend_repo.CancelFriend(req_user_id, user2_id);
            }

            return "Friend removed.";
        }
    }
}