using core.entities;
using dal.dto;

namespace bll.interfaces
{
    public interface IUserService
    {
        Task<string> ValidateUser(string email, string password);
        Task<Guid?> RegisterUser(User user);
        Task<User?> GetUserById(Guid user_id);
    }
}