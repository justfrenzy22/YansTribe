using bll.dto;
using core.entities;

namespace bll.interfaces
{
    public interface IAdminService
    {
        Task<string> ValidateLogin(string email, string password);
        VerifyTokenRes AuthAdmin(string token);
        Task<List<User>?> GetUsersAsync(int admin_id);
    }
}