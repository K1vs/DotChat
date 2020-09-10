namespace K1vs.DotChat.Messages
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Common;
    using Participants;
    using Typed;

    public interface IChatMessageInfo: ICustomizable, IVersioned
    {
        MessageType Type { get; }
        ITextMessage Text { get; }
        IQuoteMessage Quote { get; }
        IReadOnlyCollection<IMessageAttachment> MessageAttachments { get; }
        IReadOnlyCollection<IChatRefMessage> ChatRefs { get; }
        IReadOnlyCollection<IContactMessage> Contacts { get; }
        bool Immutable { get; }
    }
}