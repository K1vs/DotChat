namespace K1vs.DotChat.Models.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class ChatMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> :
        ChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>,
        IChatMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
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
        public ChatMessage()
        {
        }

        public ChatMessage(Guid messageId, DateTime timestamp, long index, Guid authorId, MessageStatus messageStatus, Guid? originalMessage, MessageType type, long version, bool immutable = false, string style = null, string metadata = null, TTextMessage text = default, TQuoteMessage quote = default, TMessageAttachmentCollection messageAttachments = default, TChatRefMessageCollection chatRefs = default, TContactMessageCollection contacts = default)
            : base(type, version, immutable, style, metadata, text, quote, messageAttachments, chatRefs, contacts)
        {
            MessageId = messageId;
            Timestamp = timestamp;
            Index = index;
            AuthorId = authorId;
            MessageStatus = messageStatus;
            OriginalMessage = originalMessage;
            Version = version;
        }

        public ChatMessage(IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> messageInfo, Guid messageId, DateTime timestamp, long index, Guid authorId, MessageStatus messageStatus, Guid? originalMessage)
            : this(messageId, timestamp, index, authorId, messageStatus, originalMessage, messageInfo.Type, messageInfo.Version, messageInfo.Immutable, messageInfo.Style, messageInfo.Metadata, messageInfo.Text, messageInfo.Quote, messageInfo.MessageAttachments, messageInfo.ChatRefs, messageInfo.Contacts)
        {              
        }

        public Guid MessageId { get; }
        public DateTime Timestamp { get; }
        public long Index { get; }
        public Guid AuthorId { get; }
        public MessageStatus MessageStatus { get; }
        public Guid? OriginalMessage { get; }
    }
}
