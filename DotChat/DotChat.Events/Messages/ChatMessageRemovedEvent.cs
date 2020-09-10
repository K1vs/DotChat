namespace K1vs.DotChat.Events.Messages
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using DotChat.Messages;
    using K1vs.DotChat.Chats;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;

    public class ChatMessageRemovedEvent: Event, IChatMessageRemovedEvent
    {
        public ChatMessageRemovedEvent()
        {
        }

        public ChatMessageRemovedEvent(Guid initiatorUserId, Guid chatId, IChatMessage message) : base(initiatorUserId)
        {
            ChatId = chatId;
            Message = message;
        }

        public Guid ChatId { get; set; }
        public IChatMessage Message { get; set; }
    }
}
