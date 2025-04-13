using core.entities;
using dal.dto;

namespace bll.interfaces
{
    public interface IUserService
    {
        Task<string> ValidateUser(string email, string password);
        Task<int?> RegisterUser(User user);
        Task<User?> GetUserById(int user_id);
    }
}