using core.requests;
using core.entities;
using core.responses;

namespace dal.interfaces
{
    public interface IAdminRepo
    {
        // bool ValidateLogin(AdminLoginReq loginModel);
        Task<List<User>> GetUsers();

        // ValidateLogin
        Task<AdminLoginRes> ValidateLogin(AdminLoginReq loginModel);
        
    }
}