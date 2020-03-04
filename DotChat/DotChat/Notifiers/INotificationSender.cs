namespace K1vs.DotChat.Notifiers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Notifications;

    public interface INotificationSender
    {
        Task NotifyUsers<TNotificationBase>(TNotificationBase notification, IEnumerable<Guid> userIds)
            where TNotificationBase: INotification;

        bool SupportNotifyChatParticipants { get; }

        Task NotifyChatParticipants<TNotificationBase>(TNotificationBase notification, Guid chatId)
            where TNotificationBase : INotification;
    }
}
