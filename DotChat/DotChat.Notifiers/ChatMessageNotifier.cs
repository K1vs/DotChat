namespace K1vs.DotChat.Notifiers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using Configuration;
    using Events.Messages;
    using Messages;
    using Messages.Typed;
    using NotificationBuilders;
    using Participants;

    public class ChatMessageNotifier: NotifierBase, IChatMessageNotifier
    {
        protected readonly IChatMessagesNotificationBuilder ChatMessagesNotificationBuilder;

        public ChatMessageNotifier(IChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatMessagesNotificationBuilder chatMessagesNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService)
        {
            ChatMessagesNotificationBuilder = chatMessagesNotificationBuilder;
        }

        public virtual async Task Handle(IChatMessageAddedEvent @event, IChatBusContext chatBusContext)
        {
            var notification = ChatMessagesNotificationBuilder.BuildChatMessageAddedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatMessageEditedEvent @event, IChatBusContext chatBusContext)
        {
            var notification = ChatMessagesNotificationBuilder.BuildChatMessageEditedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatMessageRemovedEvent @event, IChatBusContext chatBusContext)
        {
            var notification = ChatMessagesNotificationBuilder.BuildChatMessageRemovedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatMessagesReadEvent @event, IChatBusContext chatBusContext)
        {
            var notification = ChatMessagesNotificationBuilder.BuildChatMessagesReadNotification(@event);
            await Notify(@event.ChatId, notification);
        }
    }
}
