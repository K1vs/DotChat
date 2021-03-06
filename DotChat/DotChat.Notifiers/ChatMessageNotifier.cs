﻿namespace K1vs.DotChat.Notifiers
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
        protected readonly IChatMessagesNotificationBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> ChatMessagesNotificationBuilder;

        public ChatMessageNotifier(TChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatMessagesNotificationBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService)
        {
            ChatMessagesNotificationBuilder = chatMessagesNotificationBuilder;
        }

        public virtual async Task Handle(IChatMessageAddedEvent<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
        {
            var notification = ChatMessagesNotificationBuilder.BuildChatMessageAddedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatMessageEditedEvent<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
        {
            var notification = ChatMessagesNotificationBuilder.BuildChatMessageEditedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatMessageRemovedEvent<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
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
