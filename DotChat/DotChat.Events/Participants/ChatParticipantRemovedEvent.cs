namespace K1vs.DotChat.Events.Participants
{
    using System;
    using Chats;
    using K1vs.DotChat.Participants;

    public class ChatParticipantRemovedEvent: Event, IChatParticipantRemovedEvent
    {
        public ChatParticipantRemovedEvent()
        {
        }

        public ChatParticipantRemovedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant participant, IParticipationStatusModificationResult participationStatusModificationResult) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
            ParticipationStatusModificationResult = participationStatusModificationResult;
        }

        public Guid ChatId { get; set; }
        public IChatParticipant Participant { get; set; }
        public IParticipationStatusModificationResult ParticipationStatusModificationResult { get; set; }
    }
}
