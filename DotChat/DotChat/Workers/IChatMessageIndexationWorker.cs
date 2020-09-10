namespace K1vs.DotChat.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Commands.Messages;
    using Handlers;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessageIndexationWorker: 
        IHandleCommand<IIndexChatMessageCommand>
    {
    }
}
