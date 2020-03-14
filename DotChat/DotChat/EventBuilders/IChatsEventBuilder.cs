namespace K1vs.DotChat.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Events.Chats;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IChatsEventBuilder<TChat, TChatInfo, out TChatParticipantCollection, out TChatParticipant, out TChatUser, out TChatMessageInfo, out TTextMessage, out TQuoteMessage, out TMessageAttachmentCollection, out TMessageAttachment, out TChatRefMessageCollection, out TChatRefMessage, out TContactMessageCollection, out TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
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
        IChatAddedEvent<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> BuildChatAddedEvent(Guid initiatorUserId, TChat chat);
        IChatInfoEditedEvent<TChatInfo> BuildChatInfoEditedEvent(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo);
        IChatRemovedEvent<TChatInfo> BuildChatRemovedEvent(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo);
    }
}
