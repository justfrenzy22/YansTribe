using core.entities;

namespace dal.interfaces.repo
{
    public interface IAdminRepo
    {
        Task<List<FullUser>?> GetAllUsersAsync(Guid admin_id);
        // Task<int> ValidateLogin(AdminLoginReq loginModel);
    }
}