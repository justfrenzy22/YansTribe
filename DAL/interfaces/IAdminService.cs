using dal.requests;
using core.entities;
using dal.responses;

namespace dal.interfaces.service
{
    public interface IAdminService
    {
        Task<AdminLoginRes> ValidateLogin(AdminLoginReq loginModel);
        Task<AdminGetUsersRes> GetUsersAsync(UserGetRoleReq admin_id);
        // Task<List<User>> GetAllUsers(int super_admin_id);
        // Task<List<User>> GetUsersForAdmin(int admin_id);

    }
}