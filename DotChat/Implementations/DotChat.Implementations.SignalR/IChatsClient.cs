namespace K1vs.DotChat.Implementations.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Events.Chats;
    using Notifications.Chats;
    using Participants;

    public interface IChatsClient<in TPersonalizedChat, in TChatInfo, in TChatParticipantCollection, in TChatParticipant>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        Task ChatAdded(IChatAddedNotification<TPersonalizedChat, TChatParticipantCollection, TChatParticipant> notification);
        Task ChatInfoEdited(IChatInfoEditedNotification<TChatInfo> notification);
        Task ChatRemoved(IChatRemovedNotification<TChatInfo> notification);
    }
}
