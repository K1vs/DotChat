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
    using Models.Participants;
    using Participants;

    public class ChatParticipantsNotificationBuilder: IChatParticipantsNotificationBuilder
    {
        public virtual IChatParticipantAddedNotification BuildChatParticipantAddedNotification(IChatParticipantAddedEvent @event)
        {
            return new ChatParticipantAddedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant, @event.PreviousStatus);
        }

        public virtual IChatParticipantAppliedNotification BuildChatParticipantAppliedNotification(IChatParticipantAppliedEvent @event)
        {
            return new ChatParticipantAppliedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }

        public virtual IChatParticipantInvitedNotification BuildChatParticipantInvitedNotification(IChatParticipantInvitedEvent @event)
        {
            return new ChatParticipantInvitedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }

        public virtual IChatParticipantRemovedNotification BuildChatParticipantRemovedNotification(IChatParticipantRemovedEvent @event)
        {
            return new ChatParticipantRemovedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }

        public virtual IChatParticipantBlockedNotification BuildChatParticipantBlockedNotification(IChatParticipantBlockedEvent @event)
        {
            return new ChatParticipantBlockedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }

        public virtual IChatParticipantsAppendedNotification BuildChatParticipantsAppendedNotification(IChatParticipantsAppendedEvent @event)
        {
            return new ChatParticipantsAppendedNotification(@event.InitiatorUserId, @event.ChatId, @event.Added, @event.Invited);
        }

        public virtual IChatParticipantTypeChangedNotification BuildChatParticipantTypeChangedNotification(IChatParticipantTypeChangedEvent @event)
        {
            return new ChatParticipantTypeChangedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }
    }
}
