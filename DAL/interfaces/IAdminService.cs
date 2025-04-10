using dal.requests;
using core.entities;
using dal.responses;

namespace dal.interfaces.service
{
    public interface IAdminService
    {
        Task<int> ValidateLogin(AdminLoginReq loginModel);
        Task<List<User>?> GetUsersAsync(int admin_id);
        // Task<List<User>> GetAllUsers(int super_admin_id);
        // Task<List<User>> GetUsersForAdmin(int admin_id);

    }
}