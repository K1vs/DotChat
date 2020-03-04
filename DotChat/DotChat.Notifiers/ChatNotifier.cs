namespace K1vs.DotChat.Notifiers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using Configuration;
    using Events.Chats;
    using NotificationBuilders;
    using Participants;

    public class ChatNotifier<TChatNotificationsConfiguration, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant> :
        NotifierBase<TChatNotificationsConfiguration>,
        IChatNotifier<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant>
        where TChatNotificationsConfiguration : IChatNotificationsConfiguration
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        private readonly IChatsNotificationBuilder<TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant> _chatsNotificationBuilder;

        public ChatNotifier(TChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatsNotificationBuilder<TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant> chatsNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService)
        {
            _chatsNotificationBuilder = chatsNotificationBuilder;
        }

        public async Task Handle(IChatAddedEvent<TChat, TChatParticipantCollection, TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            var userIds = @event.Chat.Participants.Where(r => r.ChatParticipantStatus == ChatParticipantStatus.Active).Select(r => r.UserId);
            await NotificationRouteService.AddUsersToChat(userIds, @event.Chat.ChatId);
            var notification = _chatsNotificationBuilder.BuildChatAddedNotification(@event);
            await Notify(@event.Chat.ChatId, notification);
        }

        public async Task Handle(IChatInfoEditedEvent<TChatInfo> @event, IChatBusContext chatBusContext)
        {
            var notification = _chatsNotificationBuilder.BuildChatInfoEditedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public async Task Handle(IChatRemovedEvent<TChatInfo> @event, IChatBusContext chatBusContext)
        {
            var notification = _chatsNotificationBuilder.BuildChatRemovedNotification(@event);
            await Notify(@event.ChatId, notification);
            await NotificationRouteService.RemoveChat(@event.ChatId);
        }
    }
}
