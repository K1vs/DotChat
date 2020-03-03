namespace K1vs.DotChat.Basic.Notifications.Chats
{
    using System;
    using DotChat.Chats;
    using DotChat.Notifications;
    using DotChat.Notifications.Chats;
    using Models.Chats;

    public class ChatInfoEditedNotification: ChatInfoEditedNotification<ChatInfo>
    {
        public ChatInfoEditedNotification()
        {
        }

        public ChatInfoEditedNotification(Guid initiatorUserId, Guid chatId, ChatInfo chatInfo) : base(initiatorUserId, chatId, chatInfo)
        {
        }
    }
}
