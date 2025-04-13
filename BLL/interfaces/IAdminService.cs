using bll.dto;
using core.entities;
using core.enums;
using dal.dto;

namespace bll.interfaces
{
    public interface IAdminService
    {
        Task<string?> ValidateLogin(string email, string password);
        VerifyTokenRes AuthAdmin(string token);
        Task<VerifySuperAdminDTO> AuthSuperAdmin(string token);
        Task<List<User>?> GetUsersAsync(int admin_id);
        Task<string> ChangeRole(string user_id, string role);
        // Task<Role?> GetRole(int admin_id);
    }
}