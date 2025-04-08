using core.entities;
using dal.requests;
using dal.responses;

namespace dal.interfaces.service
{
    public interface IUserService
    {
        Task<UserRegisterRes> RegisterUser(core.entities.User user);
        Task<User?> GetUserById(int user_id);

        Task<int> ValidateUser(string email, string password);
    }
}