namespace K1vs.DotChat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Services;

    public interface IDotChat
    {
        IChatsService Chats { get; }
        IChatParticipantsService ChatParticipants { get; }
        IChatMessagesService ChatMessages { get; }
    }
}
