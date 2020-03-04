namespace K1vs.DotChat.Events.Chat
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using DotChat.Chats;
    using DotChat.Participants;

    public class ChatAddedEvent<TChat, TChatParticipantCollection, TChatParticipant>: Event, IChatAddedEvent<TChat, TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatAddedEvent()
        {
        }

        public ChatAddedEvent(Guid initiatorUserId, TChat chat) : base(initiatorUserId)
        {
            Chat = chat;
        }

        public TChat Chat { get; set; }
    }
}
