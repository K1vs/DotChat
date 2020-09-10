namespace K1vs.DotChat.Participants
{
    using System.Collections.Generic;

    public interface IHasParticipationCandidates
    {
        IReadOnlyCollection<IParticipationCandidate> ToAdd { get; }
        IReadOnlyCollection<IParticipationCandidate> ToInvite { get; }
    }
}
