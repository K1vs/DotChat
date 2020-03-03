namespace K1vs.DotChat.Notifications.Messages
{
    using System;
    using DotChat.Chats;
    using DotChat.Messages;
    using Events;

    public class ChatMessagesReadNotification: NotificationBase, IChatMessagesReadNotification
    {
        public ChatMessagesReadNotification()
        {
        }

        public ChatMessagesReadNotification(Guid initiatorUserId, Guid chatId, long index) : base(initiatorUserId)
        {
            ChatId = chatId;
            Index = index;
        }

        public Guid ChatId { get; set; }
        public long Index { get; set; }
    }
}
