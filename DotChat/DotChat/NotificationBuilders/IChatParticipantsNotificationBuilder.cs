namespace K1vs.DotChat.NotificationBuilders
{
    using System;
    using System.Collections.Generic;
    using Events.Participants;
    using Notifications.Participants;
    using Participants;

    public interface IChatParticipantsNotificationBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant>
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        IChatParticipantAddedNotification<TChatParticipant> BuildChatParticipantAddedNotification(IChatParticipantAddedEvent<TChatParticipant> @event);
        IChatParticipantAppliedNotification<TChatParticipant> BuildChatParticipantAppliedNotification(IChatParticipantAppliedEvent<TChatParticipant> @event);
        IChatParticipantInvitedNotification<TChatParticipant> BuildChatParticipantInvitedNotification(IChatParticipantInvitedEvent<TChatParticipant> @event);
        IChatParticipantRemovedNotification<TChatParticipant> BuildChatParticipantRemovedNotification(IChatParticipantRemovedEvent<TChatParticipant> @event);
        IChatParticipantBlockedNotification<TChatParticipant> BuildChatParticipantBlockedNotification(IChatParticipantBlockedEvent<TChatParticipant> @event);
        IChatParticipantsAppendedNotification<TParticipationResultCollection, TParticipationResult, TChatParticipant> BuildChatParticipantsAppendedNotification(IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant> @event);
        IChatParticipantTypeChangedNotification<TChatParticipant> BuildChatParticipantTypeChangedNotification(IChatParticipantTypeChangedEvent<TChatParticipant> @event);
    }
}
