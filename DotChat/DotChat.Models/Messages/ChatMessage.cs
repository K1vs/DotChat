namespace K1vs.DotChat.Models.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class ChatMessage: IChatMessage
    {
        public ChatMessage()
        {
        }

        public ChatMessage(Guid messageId, DateTime timestamp, long index, Guid authorId, MessageStatus messageStatus, Guid? originalMessage, bool isSystem, int version, IChatMessageInfo chatMessageInfo)
        {
            MessageId = messageId;
            Timestamp = timestamp;
            Index = index;
            AuthorId = authorId;
            MessageStatus = messageStatus;
            OriginalMessage = originalMessage;
            Version = version;
            IsSystem = isSystem;
            ChatMessageInfo = chatMessageInfo;
        }

        public Guid MessageId { get; set; }
        public DateTime Timestamp { get; set; }
        public long Index { get; set; }
        public Guid AuthorId { get; set; }
        public MessageStatus MessageStatus { get; set; }
        public Guid? OriginalMessage { get; set; }
        public bool IsSystem { get; set; }
        public IChatMessageInfo ChatMessageInfo { get; set; }
        public long Version { get; set; }
    }
}
