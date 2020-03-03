namespace K1vs.DotChat.Events.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Participants;

    public interface IChatParticipantTypeChangedEvent<out TChatParticipant> : IEventBase, IChatRelated, IHasParticipant<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
