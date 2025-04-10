using dal.requests;
using core.entities;

namespace dal.interfaces.repo
{
    public interface IAdminRepo
    {
        Task<List<User>?> GetAllUsersAsync(int admin_id);
        // Task<int> ValidateLogin(AdminLoginReq loginModel);
    }
}