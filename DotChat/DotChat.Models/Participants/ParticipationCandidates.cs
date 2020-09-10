namespace K1vs.DotChat.Models.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Participants;

    public class ParticipationCandidates: IHasParticipationCandidates
    {
        public ParticipationCandidates()
        {
        }

        public ParticipationCandidates(IReadOnlyCollection<IParticipationCandidate> add, IReadOnlyCollection<IParticipationCandidate> invite)
        {
            ToAdd = add;
            ToInvite = invite;
        }

        public IReadOnlyCollection<IParticipationCandidate> ToAdd { get; set; }
        public IReadOnlyCollection<IParticipationCandidate> ToInvite { get; set; }
    }
}
