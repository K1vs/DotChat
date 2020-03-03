namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Chats;
    using DotChat.Messages;

    public class ReadChatMessagesCommand: CommandBase, IReadChatMessagesCommand
    {
        public ReadChatMessagesCommand()
        {
        }

        public ReadChatMessagesCommand(Guid initiatorUserId, Guid chatId, long index) : base(initiatorUserId)
        {
            ChatId = chatId;
            Index = index;
        }

        public Guid ChatId { get; set; }
        public long Index { get; set; }
    }
}
