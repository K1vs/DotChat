namespace K1vs.DotChat.Notifications.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using Events;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;

    public class ChatMessageRemovedNotification: Notification, IChatMessageRemovedNotification
    {
        public ChatMessageRemovedNotification()
        {
        }

        public ChatMessageRemovedNotification(Guid initiatorUserId, Guid chatId, IChatMessage message) : base(initiatorUserId)
        {
            ChatId = chatId;
            Message = message;
        }

        public Guid ChatId { get; set; }
        public IChatMessage Message { get; set; }
    }
}
