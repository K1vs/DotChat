namespace K1vs.DotChat.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IParticipationTypeModificationResult: IHasParticipant
    {
        ChatParticipantType? PreviousType { get; }
    }
}
