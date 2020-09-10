using System;

namespace K1vs.DotChat.Messages
{
    using System.Collections.Generic;
    using Chats;
    using K1vs.DotChat.Common;
    using Participants;
    using Typed;

    public interface IChatMessage: IChatMessageInfo, IChatMessageRelated, IHasTimestamp, IIndexed, IVersioned
    {
        Guid AuthorId { get; }
        MessageStatus MessageStatus { get; }
        Guid? OriginalMessage { get; }
        bool IsSystem { get; }
    }
}