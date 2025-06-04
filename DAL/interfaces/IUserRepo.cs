using core.entities;
using core.enums;
using dal.dto;

namespace dal.interfaces.repo
{
    public interface IUserRepo
    {
        Task<Guid> RegisterUser(UserCredentials user);
        Task<UserDetails?> GetUserById(Guid user_id);
        Task<UserProfile?> GetUserProfileById(Guid req_user_id, Guid profile_user_id);
        Task<UserAccount?> GetUserEssentials(Guid user_id);
        Task<UserCredentials?> GetUserByEmail(string email);
        Task<Guid?> GetUserIdByUsername(string username);
        Task<UserCredentials?> ValidateUserByEmail(string email);
        Task<bool> ChangeRole(Guid user_id, string role);
        Task<UserCredentials?> GetUserByUsername(string username);
        Task<List<FriendNotification>?> GetFriendNotifications(Guid user_id);

    }
}