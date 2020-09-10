namespace K1vs.DotChat.Notifiers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using Configuration;
    using Events.Chats;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using NotificationBuilders;
    using Participants;

    public class ChatNotifier: NotifierBase, IChatNotifier
    {
        protected readonly IChatsNotificationBuilder ChatsNotificationBuilder;

        public ChatNotifier(IChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatsNotificationBuilder chatsNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService)
        {
            ChatsNotificationBuilder = chatsNotificationBuilder;
        }

        public virtual async Task Handle(IChatAddedEvent @event, IChatBusContext chatBusContext)
        {
            var userIds = @event.Chat.Participants.Where(r => r.ChatParticipantStatus == ChatParticipantStatus.Active).Select(r => r.UserId);
            await NotificationRouteService.AddUsersToChat(userIds, @event.Chat.ChatId);
            var notification = ChatsNotificationBuilder.BuildChatAddedNotification(@event);
            await Notify(@event.Chat.ChatId, notification);
        }

        public virtual async Task Handle(IChatInfoEditedEvent @event, IChatBusContext chatBusContext)
        {
            var notification = ChatsNotificationBuilder.BuildChatInfoEditedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatRemovedEvent @event, IChatBusContext chatBusContext)
        {
            var notification = ChatsNotificationBuilder.BuildChatRemovedNotification(@event);
            await Notify(@event.ChatId, notification);
            await NotificationRouteService.RemoveChat(@event.ChatId);
        }
    }
}
