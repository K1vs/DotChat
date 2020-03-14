namespace K1vs.DotChat.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Commands.Messages;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessagesCommandBuilder<out TChatInfo, out TChatUser, TChatMessageInfo, out TTextMessage, out TQuoteMessage, out TMessageAttachmentCollection, out TMessageAttachment, out TChatRefMessageCollection, out TChatRefMessage, out TContactMessageCollection, out TContactMessage>
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
        IIndexChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> BuildIndexChatMessageCommand(Guid currentUserId, Guid chatId, Guid? messageId, bool isSystem, TChatMessageInfo chatMessageInfo);
        IAddChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> BuildAddChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId, long index, bool isSystem, TChatMessageInfo chatMessageInfo);
        IEditChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> BuildEditChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId, TChatMessageInfo chatMessageInfo, Guid? archivedMessageId);
        IRemoveChatMessageCommand BuildRemoveChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId);
        IReadChatMessagesCommand BuildReadChatMessagesCommand(Guid currentUserId, Guid chatId, long index, bool force);
    }
}
