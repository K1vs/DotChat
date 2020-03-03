namespace K1vs.DotChat.Models.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Participants;

    public class ParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate> : IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
        public ParticipationCandidates()
        {
        }

        public ParticipationCandidates(TParticipationCandidateCollection add, TParticipationCandidateCollection invite)
        {
            ToAdd = add;
            ToInvite = invite;
        }

        public TParticipationCandidateCollection ToAdd { get; set; }
        public TParticipationCandidateCollection ToInvite { get; set; }
    }
}
