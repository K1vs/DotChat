namespace K1vs.DotChat.Models.Participants
{
    using DotChat.Participants;

    public class ParticipationResult<TChatParticipant> : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ParticipationResult()
        {
        }

        public ParticipationResult(ChatParticipantStatus? previousStatus, TChatParticipant participant)
        {
            PreviousStatus = previousStatus;
            Participant = participant;
        }

        public ChatParticipantStatus? PreviousStatus { get; set; }
        public TChatParticipant Participant { get; set; }
    }
}
