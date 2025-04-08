using core.entities;
using core.enums;
using dal.requests;

namespace dal.interfaces.repo
{
    public interface IUserRepo
    {
        Task<int> RegisterUser(User user);
        // Task<Role?> GetRoleById(UserGetRoleReq user_id);
        Task<int> ValidateUser(string email, string password);
        Task<User?> GetUserById(int user_id);
    }
}