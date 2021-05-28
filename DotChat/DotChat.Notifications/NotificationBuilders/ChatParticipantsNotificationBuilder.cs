namespace K1vs.DotChat.Basic.NotificationBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Events.Participants;
    using DotChat.NotificationBuilders;
    using DotChat.Notifications.Participants;

    public class ChatParticipantsNotificationBuilder: IChatParticipantsNotificationBuilder
    {
        public virtual IChatParticipantAddedNotification BuildChatParticipantAddedNotification(IChatParticipantAddedEvent @event)
        {
            return new ChatParticipantAddedNotification(@event.InitiatorUserId, @event.ChatId, @event.ParticipationModificationResult);
        }

        public virtual IChatParticipantAppliedNotification BuildChatParticipantAppliedNotification(IChatParticipantAppliedEvent @event)
        {
            return new ChatParticipantAppliedNotification(@event.InitiatorUserId, @event.ChatId, @event.ParticipationModificationResult);
        }

        public virtual IChatParticipantInvitedNotification BuildChatParticipantInvitedNotification(IChatParticipantInvitedEvent @event)
        {
            return new ChatParticipantInvitedNotification(@event.InitiatorUserId, @event.ChatId, @event.ParticipationModificationResult);
        }

        public virtual IChatParticipantRemovedNotification BuildChatParticipantRemovedNotification(IChatParticipantRemovedEvent @event)
        {
            return new ChatParticipantRemovedNotification(@event.InitiatorUserId, @event.ChatId, @event.ParticipationStatusModificationResult);
        }

        public virtual IChatParticipantBlockedNotification BuildChatParticipantBlockedNotification(IChatParticipantBlockedEvent @event)
        {
            return new ChatParticipantBlockedNotification(@event.InitiatorUserId, @event.ChatId, @event.ParticipationStatusModificationResult);
        }

        public virtual IChatParticipantBulkAddedInvitedNotification BuildChatParticipantBulkAddedInvitedNotification(IChatParticipantBulkAddedInvitedEvent @event)
        {
            return new ChatParticipantsAppendedNotification(@event.InitiatorUserId, @event.ChatId, @event.Added, @event.Invited);
        }

        public virtual IChatParticipantTypeChangedNotification BuildChatParticipantTypeChangedNotification(IChatParticipantTypeChangedEvent @event)
        {
            return new ChatParticipantTypeChangedNotification(@event.InitiatorUserId, @event.ChatId, @event.ParticipationTypeModificationResult);
        }

        public IChatParticipantReadNotification BuildChatMessagesReadNotification(IChatParticipantReadEvent @event)
        {
            return new ChatParticipantReadNotification(@event.InitiatorUserId, @event.ChatId, @event.Index, @event.Force);
        }
    }
}
