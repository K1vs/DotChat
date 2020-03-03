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
    using Typed;

    public class ChatMessage: ChatMessage<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatMessage()
        {
        }

        public ChatMessage(Guid messageId, DateTime timestamp, long index, Guid authorId, MessageStatus messageStatus, Guid? originalMessage, MessageType type, bool immutable = false, string style = null, string metadata = null, TextMessage text = default, QuoteMessage quote = default, List<MessageAttachment> messageAttachments = default, List<ChatRefMessage> chatRefs = default, List<ContactMessage> contacts = default) : base(messageId, timestamp, index, authorId, messageStatus, originalMessage, type, immutable, style, metadata, text, quote, messageAttachments, chatRefs, contacts)
        {
        }

        public ChatMessage(Guid messageId, DateTime timestamp, long index, Guid authorId, MessageStatus messageStatus, Guid? originalMessage, 
            IChatMessageInfo<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> messageInfo) 
            : base(messageInfo, messageId, timestamp, index, authorId, messageStatus, originalMessage)
        {
        }
    }
}
