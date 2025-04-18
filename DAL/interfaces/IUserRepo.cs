using core.entities;
using core.enums;
using dal.dto;

namespace dal.interfaces.repo
{
    public interface IUserRepo
    {
        Task<int> RegisterUser(User user);
        Task<User?> GetUserById(int user_id);
        Task<User?> GetUserByEmail(string email);
        Task<User?> ValidateUserByEmail(string email);
        Task<bool> ChangeRole(int user_id, string role);

        Task<User?> GetUserByUsername(string username);
    }
}