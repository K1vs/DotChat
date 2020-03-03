namespace K1vs.DotChat.Demo.Others
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Notifications;
    using Notifiers;

    public class EventNotificationSender: INotificationSender
    {
        public event Action<INotificationBase, Guid> OnNotification; 

        public Task NotifyUsers<TNotificationBase>(TNotificationBase notification, IEnumerable<Guid> userIds) where TNotificationBase : INotificationBase
        {
            foreach (var userId in userIds)
            {
                OnNotification?.Invoke(notification, userId);
            }
            return Task.CompletedTask;
        }

        public bool SupportNotifyChatParticipants => false;

        public async Task NotifyChatParticipants<TNotificationBase>(TNotificationBase notification, Guid chatId) where TNotificationBase : INotificationBase
        {
            await Task.Yield();
            throw new NotImplementedException();
        }
    }
}
