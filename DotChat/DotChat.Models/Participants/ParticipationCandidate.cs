namespace K1vs.DotChat.Models.Participants
{
    using System;
    using System.Collections.Generic;
    using DotChat.Participants;
    using K1vs.DotChat.Common;

    public class ParticipationCandidate: IParticipationCandidate
    {
        public ParticipationCandidate()
        {
        }

        public ParticipationCandidate(Guid userId, ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null)
        {
            UserId = userId;
            ChatParticipantType = chatParticipantType;
            Styles = styles;
        }

        public Guid UserId { get; set; }
        public ChatParticipantType ChatParticipantType { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}
