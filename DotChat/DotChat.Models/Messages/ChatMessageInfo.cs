namespace K1vs.DotChat.Models.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class ChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> :
        IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
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
        public ChatMessageInfo()
        {
        }

        public ChatMessageInfo(MessageType type, bool immutable = false, string style = null, string metadata = null, 
            TTextMessage text = default, TQuoteMessage quote = default, TMessageAttachmentCollection messageAttachments = default, TChatRefMessageCollection chatRefs = default, TContactMessageCollection contacts = default)
        {
            Type = type;
            Immutable = immutable;
            Style = style;
            Metadata = metadata;
            Text = text;
            Quote = quote;
            MessageAttachments = messageAttachments;
            ChatRefs = chatRefs;
            Contacts = contacts;
        }

        public MessageType Type { get; set; }
        public bool Immutable { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
        public TTextMessage Text { get; set; }
        public TQuoteMessage Quote { get; set; }
        public TMessageAttachmentCollection MessageAttachments { get; set; }
        public TChatRefMessageCollection ChatRefs { get; set; }
        public TContactMessageCollection Contacts { get; set; }
    }
}
