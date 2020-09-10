namespace K1vs.DotChat.Notifications.Chats
{
    using System;
    using DotChat.Chats;
    using Events;

    public class ChatRemovedNotification: Notification, IChatRemovedNotification
    {
        public ChatRemovedNotification()
        {
        }

        public ChatRemovedNotification(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
        }

        public Guid ChatId { get; set; }
        public IChatInfo ChatInfo { get; set; }
    }
}
