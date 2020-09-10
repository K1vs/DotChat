namespace K1vs.DotChat.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IHasParticipant
    {
        IChatParticipant Participant { get; }
    }
}
