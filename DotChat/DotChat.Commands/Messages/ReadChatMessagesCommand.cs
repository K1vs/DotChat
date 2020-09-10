namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Chats;
    using DotChat.Messages;

    public class ReadChatMessagesCommand: Command, IReadChatMessagesCommand
    {
        public ReadChatMessagesCommand()
        {
        }

        public ReadChatMessagesCommand(Guid initiatorUserId, Guid chatId, long index, bool force) : base(initiatorUserId)
        {
            ChatId = chatId;
            Index = index;
            Force = force;
        }

        public Guid ChatId { get; set; }
        public long Index { get; set; }
        public bool Force { get; set; }
    }
}
