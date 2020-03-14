namespace K1vs.DotChat.Notifications.Messages
{
    using System;
    using DotChat.Chats;
    using DotChat.Messages;
    using Events;

    public class ChatMessagesReadNotification: Notification, IChatMessagesReadNotification
    {
        public ChatMessagesReadNotification()
        {
        }

        public ChatMessagesReadNotification(Guid initiatorUserId, Guid chatId, long index, bool force) : base(initiatorUserId)
        {
            ChatId = chatId;
            Index = index;
            Force = force;
        }

        public Guid ChatId { get; set; }
        public long Index { get; set; }
        public bool Force { get; set; }
    }
}
