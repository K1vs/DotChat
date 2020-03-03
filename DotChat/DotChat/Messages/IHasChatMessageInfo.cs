namespace K1vs.DotChat.Messages
{
    using System.Collections.Generic;
    using Chats;
    using Participants;
    using Typed;

    public interface IHasChatMessageInfo<out TChatInfo, out TChatUser, out TChatMessageInfo, out TTextMessage, out TQuoteMessage, 
        out TMessageAttachmentCollection, out TMessageAttachment, out TChatRefMessageCollection, out TChatRefMessage, out TContactMessageCollection, out TContactMessage>
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
        TChatMessageInfo MessageInfo { get; }
    }
}
