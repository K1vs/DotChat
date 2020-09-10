namespace K1vs.DotChat.Models.Participants
{
    using DotChat.Participants;

    public class ParticipationResult<TChatParticipant> : IParticipationResult
    {
        public ParticipationResult()
        {
        }

        public ParticipationResult(ChatParticipantStatus? previousStatus, IChatParticipant participant)
        {
            PreviousStatus = previousStatus;
            Participant = participant;
        }

        public ChatParticipantStatus? PreviousStatus { get; set; }
        public IChatParticipant Participant { get; set; }
    }
}
