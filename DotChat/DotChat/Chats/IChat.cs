namespace K1vs.DotChat.Chats
{
    using K1vs.DotChat.Participants;
    using System;
    using System.Collections.Generic;
    using Messages;
    using K1vs.DotChat.Common;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Users;

    public interface IChat : IChatRelated, IHasParticipants, IVersioned
    {
        IChatInfo ChatInfo { get; set; }
        long TopIndex { get; }
        DateTime LastTimestamp { get; }
        Guid? LastMessageId { get; }
        IChatMessageInfo LastChatMessageInfo { get; }
        long LastMessageIndex { get; set; }
        Guid? LastMessageAuthorId { get; }
        IChatUser LastMessageAuthorInfo { get; set; }
        long PaticipantsCount { get; set; }
    }
}