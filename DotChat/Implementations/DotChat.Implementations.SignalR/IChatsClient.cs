namespace K1vs.DotChat.Implementations.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Events.Chats;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Notifications.Chats;
    using Participants;

    public interface IChatsClient
    {
        Task ChatAdded(IChatAddedNotification notification);
        Task ChatInfoEdited(IChatInfoEditedNotification notification);
        Task ChatRemoved(IChatRemovedNotification notification);
    }
}
