namespace K1vs.DotChat.Messages
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Common;
    using Participants;
    using Typed;

    public interface IChatMessageInfo<out TChatInfo, out TChatUser, out TChatMessageInfo, out TTextMessage, out TQuoteMessage, out TMessageAttachmentCollection, out TMessageAttachment, out TChatRefMessageCollection, out TChatRefMessage, out TContactMessageCollection, out TContactMessage>: ICustomizable
        where TChatInfo: IChatInfo
        where TChatUser: IChatUser
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage: ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection: IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment: IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage: IContactMessage<TChatUser>
    {
        MessageType Type { get; }
        TTextMessage Text { get; }
        TQuoteMessage Quote { get; }
        TMessageAttachmentCollection MessageAttachments { get; }
        TChatRefMessageCollection ChatRefs { get; }
        TContactMessageCollection Contacts { get; }
        bool Immutable { get; }
    }
}