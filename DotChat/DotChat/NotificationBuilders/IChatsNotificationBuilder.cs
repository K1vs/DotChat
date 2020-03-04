namespace K1vs.DotChat.NotificationBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Events.Chats;
    using Notifications.Chats;
    using Participants;

    public interface IChatsNotificationBuilder<out TPersonalizedChat, in TChat, TChatInfo, TChatParticipantCollection, TChatParticipant>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo: IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        IChatAddedNotification<TPersonalizedChat, TChatParticipantCollection, TChatParticipant> BuildChatAddedNotification(IChatAddedEvent<TChat, TChatParticipantCollection, TChatParticipant> @event);
        IChatInfoEditedNotification<TChatInfo> BuildChatInfoEditedNotification(IChatInfoEditedEvent<TChatInfo> @event);
        IChatRemovedNotification<TChatInfo> BuildChatRemovedNotification(IChatRemovedEvent<TChatInfo> @event);
    }
}
