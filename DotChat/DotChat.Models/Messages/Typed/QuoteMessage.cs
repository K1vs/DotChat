namespace K1vs.DotChat.Models.Messages.Typed
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using Participants;

    public class QuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> 
        : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
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
        public QuoteMessage()
        {
        }

        public QuoteMessage(Guid chatId, Guid messageId, DateTime timestamp, TChatMessageInfo messageInfo, string style = null, string metadata = null)
        {
            ChatId = chatId;
            MessageId = messageId;
            Timestamp = timestamp;
            MessageInfo = messageInfo;
            Style = style;
            Metadata = metadata;
        }

        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }
        public DateTime Timestamp { get; set; }
        public TChatMessageInfo MessageInfo { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}
