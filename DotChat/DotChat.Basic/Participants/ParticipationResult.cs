namespace K1vs.DotChat.Basic.Participants
{
    using DotChat.Participants;
    using Models.Participants;

    public class ParticipationResult : ParticipationResult<ChatParticipant>
    {
        public ParticipationResult()
        {
        }

        public ParticipationResult(ChatParticipantStatus? previousStatus, ChatParticipant participant) : base(previousStatus, participant)
        {
        }
    }
}
