using System;

namespace K1vs.DotChat.Messages
{
    using System.Collections.Generic;
    using Chats;
    using Participants;
    using Typed;

    public interface IChatMessage<out TChatInfo, out TChatUser, out TChatMessageInfo, out TTextMessage, out TQuoteMessage, out TMessageAttachmentCollection, out TMessageAttachment, out TChatRefMessageCollection, out TChatRefMessage, out TContactMessageCollection, out TContactMessage> : 
        IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>, IChatMessageRelated, IHasTimestamp, IIndexed
        where TChatInfo : IChatInfo
        where TChatUser : IChatUser
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
    {
        Guid AuthorId { get; }
        MessageStatus MessageStatus { get; }
        Guid? OriginalMessage { get; }
    }
}