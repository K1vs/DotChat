namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class EditChatMessageCommand: Command, IEditChatMessageCommand
    {
        public EditChatMessageCommand()
        {
        }

        public EditChatMessageCommand(Guid initiatorUserId, Guid chatId, Guid messageId, IChatMessageInfo messageInfo, Guid archivedMessageId) : base(initiatorUserId)
        {
            ChatId = chatId;
            MessageId = messageId;
            MessageInfo = messageInfo;
            ArchivedMessageId = archivedMessageId;
        }

        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }
        public IChatMessageInfo MessageInfo { get; set; }
        public Guid ArchivedMessageId { get; set; }
    }
}
