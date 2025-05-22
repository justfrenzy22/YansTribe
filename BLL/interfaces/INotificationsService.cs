using core.entities;

namespace bll.interfaces
{
    public interface INotificationsService
    {
        Task<Notifications> GetNotifications(Guid user_id);
    }
}