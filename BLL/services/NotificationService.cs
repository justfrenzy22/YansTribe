using bll.interfaces;
using core.entities;
using dal.interfaces.repo;

namespace bll.services
{
    public class NotificationService : INotificationsService
    {
        private readonly IUserRepo repo;

        public NotificationService(IUserRepo repo) => this.repo = repo;

        public async Task<Notifications> GetNotifications(Guid user_id)
        {
            var frNotifications = await this.repo.GetFriendNotifications(user_id);
            return new Notifications(frNotifications);
        }
    }
}