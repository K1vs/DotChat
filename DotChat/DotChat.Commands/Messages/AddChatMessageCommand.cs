namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class AddChatMessageCommand: Command, IAddChatMessageCommand
    {
        public AddChatMessageCommand()
        {
        }

        public AddChatMessageCommand(Guid initiatorUserId, Guid chatId, Guid messageId, DateTime timestamp, long index, bool isSystem, IChatMessageInfo messageInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            MessageId = messageId;
            Timestamp = timestamp;
            Index = index;
            MessageInfo = messageInfo;
            IsSystem = isSystem;
        }

        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }
        public long Index { get; set; }
        public IChatMessageInfo MessageInfo { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsSystem { get; set; }
    }
}
