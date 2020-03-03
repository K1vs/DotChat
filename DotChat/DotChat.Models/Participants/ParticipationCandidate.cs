namespace K1vs.DotChat.Models.Participants
{
    using System;
    using DotChat.Participants;

    public class ParticipationCandidate: IParticipationCandidate
    {
        public ParticipationCandidate()
        {
        }

        public ParticipationCandidate(Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null)
        {
            UserId = userId;
            ChatParticipantType = chatParticipantType;
            Style = style;
            Metadata = metadata;
        }

        public Guid UserId { get; set; }
        public ChatParticipantType ChatParticipantType { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}
