namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class EditChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>: CommandBase,
        IEditChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> 
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
        public EditChatMessageCommand()
        {
        }

        public EditChatMessageCommand(Guid initiatorUserId, Guid chatId, Guid messageId, TChatMessageInfo messageInfo, Guid archivedMessageId) : base(initiatorUserId)
        {
            ChatId = chatId;
            MessageId = messageId;
            MessageInfo = messageInfo;
            ArchivedMessageId = archivedMessageId;
        }

        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }
        public TChatMessageInfo MessageInfo { get; set; }
        public Guid ArchivedMessageId { get; set; }
    }
}
