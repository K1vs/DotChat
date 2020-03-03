namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Chats;
    using DotChat.Messages;

    public interface IReadChatMessagesCommand: ICommandBase, IChatRelated, IIndexed
    {
    }
}
