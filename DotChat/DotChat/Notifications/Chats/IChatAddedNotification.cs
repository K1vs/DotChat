namespace K1vs.DotChat.Notifications.Chats
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;
    using Participants;

    public interface IChatAddedNotification<out TPersonalizedChat, out TChatParticipantCollection,  out TChatParticipant> : INotificationBase, IHasPersonalizedChat<TPersonalizedChat, TChatParticipantCollection, TChatParticipant>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
