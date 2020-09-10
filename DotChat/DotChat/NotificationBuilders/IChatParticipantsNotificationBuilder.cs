namespace K1vs.DotChat.NotificationBuilders
{
    using System;
    using System.Collections.Generic;
    using Events.Participants;
    using Notifications.Participants;
    using Participants;

    public interface IChatParticipantsNotificationBuilder
    {
        IChatParticipantAddedNotification BuildChatParticipantAddedNotification(IChatParticipantAddedEvent @event);
        IChatParticipantAppliedNotification BuildChatParticipantAppliedNotification(IChatParticipantAppliedEvent @event);
        IChatParticipantInvitedNotification BuildChatParticipantInvitedNotification(IChatParticipantInvitedEvent @event);
        IChatParticipantRemovedNotification BuildChatParticipantRemovedNotification(IChatParticipantRemovedEvent @event);
        IChatParticipantBlockedNotification BuildChatParticipantBlockedNotification(IChatParticipantBlockedEvent @event);
        IChatParticipantsAppendedNotification BuildChatParticipantsAppendedNotification(IChatParticipantsAppendedEvent @event);
        IChatParticipantTypeChangedNotification BuildChatParticipantTypeChangedNotification(IChatParticipantTypeChangedEvent @event);
    }
}
