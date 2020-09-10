namespace K1vs.DotChat.Workers
{
    using System.Collections.Generic;
    using Chats;
    using Commands.Chats;
    using Events.Messages;
    using Handlers;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatsWorker:
        IHandleCommand<IAddChatCommand>,
        IHandleCommand<IEditChatInfoCommand>,
        IHandleCommand<IRemoveChatCommand>,
        IHandleEvent<IChatMessageAddedEvent>
    {
    }
}
