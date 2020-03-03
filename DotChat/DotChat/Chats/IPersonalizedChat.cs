namespace K1vs.DotChat.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Participants;

    public interface IPersonalizedChat<out TChatParticipantCollection, out TChatParticipant> : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        long ReadIndex { get; }
        long UnreadCount { get; }
    }
}
