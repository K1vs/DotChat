namespace K1vs.DotChat.Events.Messages
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using DotChat.Chats;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;

    public class ChatMessageAddedEvent: Event, IChatMessageAddedEvent
    {
        public ChatMessageAddedEvent()
        {
        }

        public ChatMessageAddedEvent(Guid initiatorUserId, Guid chatId, IChatMessage message) : base(initiatorUserId)
        {
            ChatId = chatId;
            Message = message;
        }

        public Guid ChatId { get; set; }
        public IChatMessage Message { get; set; }
    }
}
