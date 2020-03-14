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

    public class ChatNotifier<TChatNotificationsConfiguration, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> :
        NotifierBase<TChatNotificationsConfiguration>,
        IChatNotifier<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatNotificationsConfiguration : IChatNotificationsConfiguration
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TChatUser : IChatUser
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
    {
        private readonly IChatsNotificationBuilder<TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> _chatsNotificationBuilder;

        public ChatNotifier(TChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatsNotificationBuilder<TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatsNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService)
        {
            _chatsNotificationBuilder = chatsNotificationBuilder;
        }

        public async Task Handle(IChatAddedEvent<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
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
