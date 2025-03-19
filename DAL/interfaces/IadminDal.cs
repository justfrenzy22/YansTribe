using core.requests;
using core.entities;
using core.responses;

namespace dal.interfaces
{
    public interface IAdminDal
    {
        Task<AdminLoginRes> ValidateLogin(AdminLoginReq loginModel);
        Task<List<User>> GetUsers();
    }
}