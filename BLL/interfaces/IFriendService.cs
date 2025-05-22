namespace bll.interfaces
{
    public interface IFriendService
    {
        Task AddFriend(Guid req_user_id, Guid user2_id);
        Task<string?> RemoveFriend(Guid req_user_id, Guid user2_id);
        Task<string?> CancelFriend(Guid req_user_id, Guid user2_id);
        Task<string?> AcceptFriend(Guid req_user_id, Guid user2_id);
        Task<string?> DeclineFriend(Guid req_user_id, Guid user2_id);
    }
}