namespace K1vs.DotChat.Notifiers
{
    using System.Collections.Generic;
    using Chats;
    using Events.Chats;
    using Handlers;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IChatNotifier:
        IHandleEvent<IChatAddedEvent>,
        IHandleEvent<IChatInfoEditedEvent>,
        IHandleEvent<IChatRemovedEvent>
    {
    }
}
