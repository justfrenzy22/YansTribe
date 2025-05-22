using bll.dto;
using core.entities;

namespace bll.interfaces
{
    public interface IUserService
    {
        Task<string> ValidateUser(string email, string password);
        Task<Guid?> RegisterUser(FullUser user);
        Task<ProfileUser?> FetchUserProfile(string username, Guid req_user_id);
        Task<SafeUser?> GetUserById(Guid user_id);
        Task<BaseUser?> GetUserEssentials(Guid user_id);
        VerifyTokenRes AuthUser(string token);
    }
}