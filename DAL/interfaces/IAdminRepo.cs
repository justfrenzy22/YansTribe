using core.entities;

namespace dal.interfaces.repo
{
    public interface IAdminRepo
    {
        Task<List<UserCredentials>?> GetAllUsersAsync(Guid admin_id);
    }
}