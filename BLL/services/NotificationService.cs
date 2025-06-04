using bll.interfaces;
using core.entities;
using dal.interfaces.repo;

namespace bll.services
{
    public class NotificationService : INotificationsService
    {
        private readonly IUserRepo _user_repo;

        public NotificationService(IUserRepo user_repo) => this._user_repo = user_repo;

        public async Task<Notifications> GetNotifications(Guid user_id)
        {
            var frNotifications = await this._user_repo.GetFriendNotifications(user_id);
            return new Notifications(frNotifications);
        }
    }
}