namespace K1vs.DotChat.Notifications.Messages
{
    using System;
    using DotChat.Chats;
    using DotChat.Messages;
    using Events;

    public class ChatMessageRemovedNotification: NotificationBase, IChatMessageRemovedNotification
    {
        public ChatMessageRemovedNotification()
        {
        }

        public ChatMessageRemovedNotification(Guid initiatorUserId, Guid chatId, Guid messageId) : base(initiatorUserId)
        {
            ChatId = chatId;
            MessageId = messageId;
        }

        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }
    }
}
