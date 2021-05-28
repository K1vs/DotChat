namespace K1vs.DotChat.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IParticipationStatusModificationResult: IHasParticipant
    {
        ChatParticipantStatus? PreviousStatus { get; }
    }
}
