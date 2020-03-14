namespace K1vs.DotChat.Basic.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using Models.Chats;
    using Models.Messages;
    using Models.Messages.Typed;
    using Models.Participants;
    using K1vs.DotChat.Basic.Messages.Typed;

    public class ChatMessage : ChatMessage<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatMessage()
        {
        }

        public ChatMessage(Guid messageId, DateTime timestamp, long index, Guid authorId, MessageStatus messageStatus, Guid? originalMessage, bool isSystem, IChatMessageInfo<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> messageInfo) : base(messageId, timestamp, index, authorId, messageStatus, originalMessage, isSystem, messageInfo)
        {
        }

        public ChatMessage(Guid messageId, DateTime timestamp, long index, Guid authorId, MessageStatus messageStatus, Guid? originalMessage, bool isSystem, MessageType type, long version, bool immutable = false, string style = null, string metadata = null, TextMessage text = null, QuoteMessage quote = null, List<MessageAttachment> messageAttachments = null, List<ChatRefMessage> chatRefs = null, List<ContactMessage> contacts = null) : base(messageId, timestamp, index, authorId, messageStatus, originalMessage, isSystem, type, version, immutable, style, metadata, text, quote, messageAttachments, chatRefs, contacts)
        {
        }
    }
}
