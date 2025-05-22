namespace core.entities
{
    public class Notifications
    {
        private List<FriendNotification>? _friend_notifications;

        public Notifications(List<FriendNotification>? friend_notifications) => this._friend_notifications = friend_notifications;

        public List<FriendNotification>? FriendNotifications => this._friend_notifications;
    }
}