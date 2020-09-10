namespace K1vs.DotChat.Models.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class ChatMessageInfo: IChatMessageInfo
    {
        public ChatMessageInfo()
        {
        }

        public ChatMessageInfo(MessageType type, long version, bool immutable = false, string style = null, string metadata = null, 
            ITextMessage text = default, IQuoteMessage quote = default, IReadOnlyCollection<IMessageAttachment> messageAttachments = default, IReadOnlyCollection<IChatRefMessage> chatRefs = default, IReadOnlyCollection<IContactMessage> contacts = default)
        {
            Type = type;
            Version = version;
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
        public long Version { get; set; }
        public bool Immutable { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
        public ITextMessage Text { get; set; }
        public IQuoteMessage Quote { get; set; }
        public IReadOnlyCollection<IMessageAttachment> MessageAttachments { get; set; }
        public IReadOnlyCollection<IChatRefMessage> ChatRefs { get; set; }
        public IReadOnlyCollection<IContactMessage> Contacts { get; set; }
    }
}
