namespace K1vs.DotChat.Commands.Chats
{
    using System;
    using DotChat.Chats;

    public class RemoveChatCommand: CommandBase, IRemoveChatCommand
    {
        public RemoveChatCommand()
        {
        }

        public RemoveChatCommand(Guid initiatorUserId, Guid chatId) : base(initiatorUserId)
        {
            ChatId = chatId;
        }

        public Guid ChatId { get; set; }
    }
}
