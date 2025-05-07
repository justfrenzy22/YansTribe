using core.entities;
using core.enums;
using dal.dto;

namespace dal.interfaces.repo
{
    public interface IUserRepo
    {
        Task<Guid> RegisterUser(User user);
        Task<User?> GetUserById(Guid user_id);
        Task<User?> GetUserByEmail(string email);
        Task<User?> ValidateUserByEmail(string email);
        Task<bool> ChangeRole(Guid user_id, string role);
        Task<User?> GetUserEssentials(Guid user_id);
        Task<Guid?> GetUserIdByUsername(string username);
        Task<User?> GetUserByUsername(string username);
    }
}