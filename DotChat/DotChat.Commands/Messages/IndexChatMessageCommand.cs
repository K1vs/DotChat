namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class IndexChatMessageCommand: Command, IIndexChatMessageCommand
    {
        public IndexChatMessageCommand()
        {
        }

        public IndexChatMessageCommand(Guid initiatorUserId, Guid chatId, Guid messageId, bool isSystem, IChatMessageInfo messageInfo) : base(initiatorUserId)
        {
            MessageId = messageId;
            ChatId = chatId;
            MessageInfo = messageInfo;
            IsSystem = isSystem;
        }

        public Guid MessageId { get; set; }

        public Guid ChatId { get; set; }

        public IChatMessageInfo MessageInfo { get; set; }

        public bool IsSystem { get; set; }
    }
}
