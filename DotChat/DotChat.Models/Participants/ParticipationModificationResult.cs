namespace K1vs.DotChat.Models.Participants
{
    using DotChat.Participants;

    public class ParticipationModificationResult: IParticipationModificationResult
    {
        public ParticipationModificationResult()
        {
        }

        public ParticipationModificationResult(IChatParticipant participant, ChatParticipantStatus? previousStatus, ChatParticipantType? previousType)
        {
            Participant = participant;
            PreviousStatus = previousStatus;
            PreviousType = previousType;
        }

        public IChatParticipant Participant { get; set; }

        public ChatParticipantStatus? PreviousStatus { get; set; }

        public ChatParticipantType? PreviousType { get; set; }
    }
}
