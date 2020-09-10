namespace K1vs.DotChat.Events.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Participants;

    public class ChatParticipantTypeChangedEvent: Event, IChatParticipantTypeChangedEvent
    {
        public ChatParticipantTypeChangedEvent()
        {
        }

        public ChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant participant) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
        }

        public Guid ChatId { get; set; }
        public IChatParticipant Participant { get; set; }
    }
}
