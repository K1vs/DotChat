namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using DotChat.Chats;
    using DotChat.Messages;

    public class RemoveChatMessageCommand: Command, IRemoveChatMessageCommand
    {
        public RemoveChatMessageCommand()
        {
        }

        public RemoveChatMessageCommand(Guid initiatorUserId, Guid chatId, Guid messageId) : base(initiatorUserId)
        {
            ChatId = chatId;
            MessageId = messageId;
        }

        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }
    }
}
