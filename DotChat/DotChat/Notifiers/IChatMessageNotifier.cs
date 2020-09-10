namespace K1vs.DotChat.Notifiers
{
    using System.Collections.Generic;
    using Chats;
    using Events.Messages;
    using Handlers;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessageNotifier:
        IHandleEvent<IChatMessageAddedEvent>,
        IHandleEvent<IChatMessageEditedEvent>,
        IHandleEvent<IChatMessageRemovedEvent>,
        IHandleEvent<IChatMessagesReadEvent>
    {
    }
}
