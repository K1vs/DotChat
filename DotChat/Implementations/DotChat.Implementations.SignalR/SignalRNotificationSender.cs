namespace K1vs.DotChat.Implementations.SignalR
{
    using K1vs.DotChat.Notifications;
    using K1vs.DotChat.Notifiers;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SignalRNotificationSender<THub> : INotificationSender
        where THub : IHub
    {
        private readonly Lazy<IHubContext> _lazyHubContext = new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<THub>());

        public bool SupportNotifyChatParticipants => false;

        protected IHubContext HubContext => _lazyHubContext.Value;

        public Task NotifyChatParticipants<TNotificationBase>(TNotificationBase notification, Guid chatId) where TNotificationBase : INotificationBase
        {
            throw new NotImplementedException();
        }

        public async Task NotifyUsers<TNotificationBase>(TNotificationBase notification, IEnumerable<Guid> userIds) where TNotificationBase : INotificationBase
        {
            foreach (var userId in userIds)
            {
                IClientProxy clientProxy = HubContext.Clients.User(userId.ToString());
                await clientProxy.Invoke(GetClientMethodName(notification), notification);
            }
        }

        protected virtual string TruncatedMethodSuffix => "Notification";

        private string GetClientMethodName<TNotificationBase>(TNotificationBase notification)
            where TNotificationBase : INotificationBase
        {
            var notificationType = notification.GetType();
            if (!string.IsNullOrEmpty(TruncatedMethodSuffix) && notificationType.Name.EndsWith(TruncatedMethodSuffix))
            {
                return notificationType.Name.Substring(0, notificationType.Name.Length - TruncatedMethodSuffix.Length);
            }
            else
            {
                return notificationType.Name;
            }
        }
    }
}
