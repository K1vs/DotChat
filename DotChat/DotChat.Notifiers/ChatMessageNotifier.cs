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

    public class ChatMessageNotifier<TChatNotificationsConfiguration, TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> :
        NotifierBase<TChatNotificationsConfiguration>,
        IChatMessageNotifier<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatNotificationsConfiguration : IChatNotificationsConfiguration
        where TChatInfo : IChatInfo
        where TChatUser : IChatUser
        where TChatMessage : IChatMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
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
        private readonly IChatMessagesNotificationBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> _chatMessagesNotificationBuilder;

        public ChatMessageNotifier(TChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatMessagesNotificationBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService)
        {
            _chatMessagesNotificationBuilder = chatMessagesNotificationBuilder;
        }

        public async Task Handle(IChatMessageAddedEvent<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
        {
            var notification = _chatMessagesNotificationBuilder.BuildChatMessageAddedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public async Task Handle(IChatMessageEditedEvent<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
        {
            var notification = _chatMessagesNotificationBuilder.BuildChatMessageEditedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public async Task Handle(IChatMessageRemovedEvent<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
        {
            var notification = _chatMessagesNotificationBuilder.BuildChatMessageRemovedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public async Task Handle(IChatMessagesReadEvent @event, IChatBusContext chatBusContext)
        {
            var notification = _chatMessagesNotificationBuilder.BuildChatMessagesReadNotification(@event);
            await Notify(@event.ChatId, notification);
        }
    }
}
