namespace K1vs.DotChat.Notifications.Chats
{
    using System;
    using DotChat.Chats;
    using Events;

    public class ChatInfoEditedNotification<TChatInfo>: NotificationBase, IChatInfoEditedNotification<TChatInfo>
        where TChatInfo : IChatInfo
    {
        public ChatInfoEditedNotification()
        {
        }

        public ChatInfoEditedNotification(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
        }

        public Guid ChatId { get; set; }
        public TChatInfo ChatInfo { get; set; }
    }
}
