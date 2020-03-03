namespace K1vs.DotChat.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Events.Participants;

    public interface IParticipationResult<out TChatParticipant>: IHasParticipant<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        ChatParticipantStatus? PreviousStatus { get; }
    }
}
