namespace K1vs.DotChat.Events.Chat
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public class ChatAddedEvent: Event, IChatAddedEvent
    {
        public ChatAddedEvent()
        {
        }

        public ChatAddedEvent(Guid initiatorUserId, IChat chat) : base(initiatorUserId)
        {
            Chat = chat;
        }

        public IChat Chat { get; set; }
    }
}
