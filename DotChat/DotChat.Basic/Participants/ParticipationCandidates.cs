namespace K1vs.DotChat.Basic.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Participants;
    using Models.Participants;

    public class ParticipationCandidates : ParticipationCandidates<List<ParticipationCandidate>, ParticipationCandidate>
    {
        public ParticipationCandidates()
        {
        }


        public ParticipationCandidates(List<ParticipationCandidate> add, List<ParticipationCandidate> invite) : base(add, invite)
        {
        }
    }
}
