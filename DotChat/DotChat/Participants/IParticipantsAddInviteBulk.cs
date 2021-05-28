namespace K1vs.DotChat.Participants
{
    using System.Collections.Generic;

    public interface IParticipantsAddInviteBulk
    {
        IReadOnlyCollection<IParticipationCandidate> ToAdd { get; }
        IReadOnlyCollection<IParticipationCandidate> ToInvite { get; }
    }
}
