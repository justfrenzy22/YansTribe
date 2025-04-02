using dal.requests;
using core.entities;
using dal.responses;

namespace dal.interfaces
{
    public interface IAdminRepo
    {
        Task<AdminLoginRes> ValidateLogin(AdminLoginReq loginModel);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetStandardUsersAsync();
    }
}