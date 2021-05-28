using System;

namespace K1vs.DotChat.Messages
{
    using System.Collections.Generic;
    using Chats;
    using K1vs.DotChat.Common;
    using Participants;
    using Typed;

    public interface IChatMessage: IChatMessageRelated, IHasTimestamp, IIndexed, IVersioned
    {
        IChatMessageInfo ChatMessageInfo { get; set; }
        Guid AuthorId { get; }
        MessageStatus MessageStatus { get; }
        Guid? OriginalMessage { get; }
        bool IsSystem { get; }
    }
}