using dal.requests;
using core.entities;

namespace dal.interfaces.repo
{
    public interface IAdminRepo
    {
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetStandardUsersAsync();
        Task<int> ValidateLogin(AdminLoginReq loginModel);
    }
}