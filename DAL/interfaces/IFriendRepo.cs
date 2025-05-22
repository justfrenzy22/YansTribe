using core.entities;

namespace dal.interfaces.repo
{
    public interface IFriendRepo
    {
        Task<Friend?> GetFriendshipStatus(Guid req_user_id, Guid friend_id);
        Task AddFriend(Guid req_user_id, Guid user2_id);
        Task AcceptFriend(Guid friendship_id);
        Task<string?> CancelFriend(Guid req_user_id, Guid user2_id);
        Task<string?> RemoveFriend(Guid req_user_id, Guid user2_id);
        Task RejectFriend(Guid friendship_id);
    }
}