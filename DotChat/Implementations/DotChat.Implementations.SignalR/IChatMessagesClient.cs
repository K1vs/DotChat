namespace K1vs.DotChat.Implementations.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Events.Messages;
    using Messages;
    using Messages.Typed;
    using Notifications.Messages;
    using Participants;

    public interface IChatMessagesClient
    {
        Task ChatMessageAdded(IChatMessageAddedNotification notification);

        Task ChatMessageEdited(IChatMessageEditedNotification notification);

        Task ChatMessageRemoved(IChatMessageRemovedNotification notification);

        Task ChatMessagesRead(IChatMessagesReadNotification notification);
    }
}
