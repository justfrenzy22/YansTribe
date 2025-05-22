using core.entities;
using core.enums;
using dal.dto;

namespace dal.interfaces.repo
{
    public interface IUserRepo
    {
        Task<Guid> RegisterUser(FullUser user);
        Task<SafeUser?> GetUserById(Guid user_id);
        Task<ProfileUser?> GetUserProfileById(Guid req_user_id, Guid profile_user_id);
        Task<BaseUser?> GetUserEssentials(Guid user_id);
        Task<FullUser?> GetUserByEmail(string email);
        Task<Guid?> GetUserIdByUsername(string username);
        Task<FullUser?> ValidateUserByEmail(string email);
        Task<bool> ChangeRole(Guid user_id, string role);
        Task<FullUser?> GetUserByUsername(string username);
        Task<List<FriendNotification>?> GetFriendNotifications(Guid user_id);

    }
}