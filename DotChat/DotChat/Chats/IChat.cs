namespace K1vs.DotChat.Chats
{
    using K1vs.DotChat.Participants;
    using System;
    using System.Collections.Generic;
    using Messages;
    using K1vs.DotChat.Common;
    using K1vs.DotChat.Messages.Typed;

    public interface IChat : IChatInfo, IChatRelated, IHasParticipants
    {
        long TopIndex { get; }
        DateTime LastTimestamp { get; }
        Guid? LastMessageId { get; }
        Guid? LastMessageAuthorId { get; }
        IChatMessageInfo LastChatMessageInfo { get; }
    }
}