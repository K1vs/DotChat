namespace K1vs.DotChat.Notifications.Chats
{
    using System;
    using DotChat.Chats;
    using Events;

    public class ChatRemovedNotification<TChatInfo>: Notification, IChatRemovedNotification<TChatInfo>
        where TChatInfo : IChatInfo
    {
        public ChatRemovedNotification()
        {
        }

        public ChatRemovedNotification(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
        }

        public Guid ChatId { get; set; }
        public TChatInfo ChatInfo { get; set; }
    }
}
