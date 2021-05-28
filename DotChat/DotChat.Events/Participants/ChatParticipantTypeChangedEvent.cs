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

        public ChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant participant, IParticipationTypeModificationResult participationTypeModificationResult) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
            ParticipationTypeModificationResult = participationTypeModificationResult;
        }

        public Guid ChatId { get; set; }
        public IChatParticipant Participant { get; set; }
        public IParticipationTypeModificationResult ParticipationTypeModificationResult { get; set; }
    }
}
