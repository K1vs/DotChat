namespace K1vs.DotChat.Events.Participants
{
    using System;
    using Chats;
    using K1vs.DotChat.Participants;

    public class ChatParticipantAppliedEvent: Event, IChatParticipantAppliedEvent
    {
        public ChatParticipantAppliedEvent()
        {
        }

        public ChatParticipantAppliedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant participant, IParticipationModificationResult participationModificationResult) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
            ParticipationModificationResult = participationModificationResult;
        }

        public Guid ChatId { get; set; }
        public IChatParticipant Participant { get; set; }
        public IParticipationModificationResult ParticipationModificationResult { get; set; }
    }
}
