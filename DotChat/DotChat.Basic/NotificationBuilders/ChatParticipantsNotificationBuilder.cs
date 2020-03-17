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
    using Notifications.Participants;
    using Participants;

    public class ChatParticipantsNotificationBuilder: IChatParticipantsNotificationBuilder<List<ParticipationResult>, ParticipationResult, ChatParticipant>
    {
        public virtual IChatParticipantAddedNotification<ChatParticipant> BuildChatParticipantAddedNotification(IChatParticipantAddedEvent<ChatParticipant> @event)
        {
            return new ChatParticipantAddedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant, @event.PreviousStatus);
        }

        public virtual IChatParticipantAppliedNotification<ChatParticipant> BuildChatParticipantAppliedNotification(IChatParticipantAppliedEvent<ChatParticipant> @event)
        {
            return new ChatParticipantAppliedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }

        public virtual IChatParticipantInvitedNotification<ChatParticipant> BuildChatParticipantInvitedNotification(IChatParticipantInvitedEvent<ChatParticipant> @event)
        {
            return new ChatParticipantInvitedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }

        public virtual IChatParticipantRemovedNotification<ChatParticipant> BuildChatParticipantRemovedNotification(IChatParticipantRemovedEvent<ChatParticipant> @event)
        {
            return new ChatParticipantRemovedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }

        public virtual IChatParticipantBlockedNotification<ChatParticipant> BuildChatParticipantBlockedNotification(IChatParticipantBlockedEvent<ChatParticipant> @event)
        {
            return new ChatParticipantBlockedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }

        public virtual IChatParticipantsAppendedNotification<List<ParticipationResult>, ParticipationResult, ChatParticipant> BuildChatParticipantsAppendedNotification(
            IChatParticipantsAppendedEvent<List<ParticipationResult>, ParticipationResult, ChatParticipant> @event)
        {
            return new ChatParticipantsAppendedNotification(@event.InitiatorUserId, @event.ChatId, @event.Added, @event.Invited);
        }

        public virtual IChatParticipantTypeChangedNotification<ChatParticipant> BuildChatParticipantTypeChangedNotification(IChatParticipantTypeChangedEvent<ChatParticipant> @event)
        {
            return new ChatParticipantTypeChangedNotification(@event.InitiatorUserId, @event.ChatId, @event.Participant);
        }
    }
}
