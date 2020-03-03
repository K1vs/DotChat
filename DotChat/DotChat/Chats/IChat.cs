namespace K1vs.DotChat.Chats
{
    using K1vs.DotChat.Participants;
    using System;
    using System.Collections.Generic;
    using Messages;

    public interface IChat<out TChatParticipantCollection, out TChatParticipant> : IChatInfo, IChatRelated, IHasParticipants<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        long TopIndex { get; }
        DateTime LastTimestamp { get; }
    }
}