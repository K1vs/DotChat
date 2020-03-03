namespace K1vs.DotChat.Participants
{
    using System.Collections.Generic;

    public interface IHasParticipationCandidates<out TParticipationCandidateCollection, out TParticipationCandidate>
        where TParticipationCandidateCollection: IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate: IParticipationCandidate
    {
        TParticipationCandidateCollection ToAdd { get; }
        TParticipationCandidateCollection ToInvite { get; }
    }
}
