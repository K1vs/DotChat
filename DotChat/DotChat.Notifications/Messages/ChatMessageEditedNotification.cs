namespace K1vs.DotChat.Notifications.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using Events;

    public class ChatMessageEditedNotification: Notification, IChatMessageEditedNotification
    {
        public ChatMessageEditedNotification()
        {
        }

        public ChatMessageEditedNotification(Guid initiatorUserId, Guid chatId, IChatMessage message) : base(initiatorUserId)
        {
            ChatId = chatId;
            Message = message;
        }

        public Guid ChatId { get; set; }
        public IChatMessage Message { get; set; }
    }
}
