namespace K1vs.DotChat.Basic.Notifications.Chats
{
    using System;
    using DotChat.Chats;
    using DotChat.Notifications;
    using DotChat.Notifications.Chats;
    using Models.Chats;

    public class ChatRemovedNotification: ChatRemovedNotification<ChatInfo>
    {
        public ChatRemovedNotification()
        {
        }

        public ChatRemovedNotification(Guid initiatorUserId, Guid chatId, ChatInfo chatInfo) : base(initiatorUserId, chatId, chatInfo)
        {
        }
    }
}
