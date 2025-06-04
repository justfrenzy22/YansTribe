using bll.dto;
using core.entities;

namespace bll.interfaces
{
    public interface IUserService
    {
        Task<string> ValidateUser(string email, string password);
        Task<Guid?> RegisterUser(UserCredentials user);
        Task<UserProfile?> FetchUserProfile(string username, Guid req_user_id);
        Task<UserDetails?> GetUserById(Guid user_id);
        Task<UserAccount?> GetUserEssentials(Guid user_id);
        VerifyTokenRes AuthUser(string token);
    }
}