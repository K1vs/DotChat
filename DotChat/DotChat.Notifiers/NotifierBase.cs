namespace K1vs.DotChat.Notifiers
{
    using System;
    using System.Threading.Tasks;
    using Configuration;
    using Notifications;

    public class NotifierBase
    {
        protected readonly IChatNotificationsConfiguration ChatNotificationsConfiguration;
        protected readonly INotificationSender NotificationSender;
        protected readonly INotificationRouteService NotificationRouteService;

        public NotifierBase(IChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService)
        {
            ChatNotificationsConfiguration = chatNotificationsConfiguration;
            NotificationSender = notificationSender;
            NotificationRouteService = notificationRouteService;
        }

        protected virtual async Task Notify<TNotification>(Guid chatId, TNotification notification)
            where TNotification : INotification
        {
            if (NotificationSender.SupportNotifyChatParticipants)
            {
                await NotificationSender.NotifyChatParticipants(notification, chatId).ConfigureAwait(false);
            }
            else
            {
                var chatUsers = await NotificationRouteService.GetChatUsers(chatId).ConfigureAwait(false);
                await NotificationSender.NotifyUsers(notification, chatUsers).ConfigureAwait(false);
            }
        }
    }
}
