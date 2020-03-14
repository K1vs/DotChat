namespace K1vs.DotChat.Chats
{
    using K1vs.DotChat.Participants;
    using System;
    using System.Collections.Generic;
    using Messages;
    using K1vs.DotChat.Common;
    using K1vs.DotChat.Messages.Typed;

    public interface IChat<out TChatInfo, out TChatParticipantCollection, out TChatParticipant, out TChatUser, out TChatMessageInfo, out TTextMessage, out TQuoteMessage, out TMessageAttachmentCollection, out TMessageAttachment, out TChatRefMessageCollection, out TChatRefMessage, out TContactMessageCollection, out TContactMessage> : IChatInfo, IChatRelated, IHasParticipants<TChatParticipantCollection, TChatParticipant>
        where TChatInfo: IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
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
        long TopIndex { get; }
        DateTime LastTimestamp { get; }
        Guid? LastMessageId { get; }
        Guid? LastMessageAuthorId { get; }
        TChatMessageInfo LastChatMessageInfo { get; }
    }
}