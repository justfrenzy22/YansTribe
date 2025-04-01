using core.entities;
using core.enums;

namespace dal.interfaces
{
    public interface IUserService
    {
        Task<int> AddUser(User user);
        Task<Role?> GetRoleById(int user_id);
        // Task<User> GetByEmail(string email);
        Task<User?> GetUserById(int user_id);

        Task<int?> ValidateUser(string email, string password);
    }
}