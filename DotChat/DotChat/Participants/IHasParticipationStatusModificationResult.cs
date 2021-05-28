namespace K1vs.DotChat.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IHasParticipationStatusModificationResult
    {
        IParticipationStatusModificationResult ParticipationStatusModificationResult { get; set; }
    }
}
