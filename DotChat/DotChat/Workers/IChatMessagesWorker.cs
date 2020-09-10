namespace K1vs.DotChat.Workers
{
    using System.Collections.Generic;
    using Chats;
    using Commands.Messages;
    using Handlers;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessagesWorker:
        IHandleCommand<IAddChatMessageCommand>,
        IHandleCommand<IEditChatMessageCommand>,
        IHandleCommand<IRemoveChatMessageCommand>,
        IHandleCommand<IReadChatMessagesCommand>
    {
    }
}
