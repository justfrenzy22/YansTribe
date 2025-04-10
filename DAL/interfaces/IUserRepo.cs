using core.entities;

namespace dal.interfaces.repo
{
    public interface IUserRepo
    {
        Task<int> RegisterUser(User user);
        Task<User?> GetUserById(int user_id);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByUsername(string username);
    }
}