namespace K1vs.DotChat.Notifiers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bus;
    using Configuration;
    using Events.Participants;
    using NotificationBuilders;
    using Participants;

    public class ChatParticipantNotifier<TChatNotificationsConfiguration, TParticipationResultCollection, TParticipationResult, TChatParticipant> : NotifierBase<TChatNotificationsConfiguration>,
        IChatParticipantNotifier<TParticipationResultCollection, TParticipationResult, TChatParticipant>
        where TChatNotificationsConfiguration : IChatNotificationsConfiguration
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        protected readonly IChatParticipantsNotificationBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant> ChatParticipantsNotificationBuilder;

        public ChatParticipantNotifier(TChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatParticipantsNotificationBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant> chatParticipantsNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService)
        {
            ChatParticipantsNotificationBuilder = chatParticipantsNotificationBuilder;
        }

        public virtual async Task Handle(IChatParticipantAddedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            await NotificationRouteService.AddUserToChat(@event.Participant.UserId, @event.ChatId);
            var notification = ChatParticipantsNotificationBuilder.BuildChatParticipantAddedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatParticipantInvitedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            var notification = ChatParticipantsNotificationBuilder.BuildChatParticipantInvitedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatParticipantAppliedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            var notification = ChatParticipantsNotificationBuilder.BuildChatParticipantAppliedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatParticipantRemovedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            var notification = ChatParticipantsNotificationBuilder.BuildChatParticipantRemovedNotification(@event);
            await Notify(@event.ChatId, notification);
            await NotificationRouteService.RemoveUserFromChat(@event.Participant.UserId, @event.ChatId);
        }

        public virtual async Task Handle(IChatParticipantBlockedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            var notification = ChatParticipantsNotificationBuilder.BuildChatParticipantBlockedNotification(@event);
            await Notify(@event.ChatId, notification);
            await NotificationRouteService.RemoveUserFromChat(@event.Participant.UserId, @event.ChatId);
        }

        public virtual async Task Handle(IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            var users = @event.Added.Select(r => r.Participant.UserId);
            await NotificationRouteService.AddUsersToChat(users, @event.ChatId);
            var notification = ChatParticipantsNotificationBuilder.BuildChatParticipantsAppendedNotification(@event);
            await Notify(@event.ChatId, notification);
        }

        public virtual async Task Handle(IChatParticipantTypeChangedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            var notification = ChatParticipantsNotificationBuilder.BuildChatParticipantTypeChangedNotification(@event);
            await Notify(@event.ChatId, notification);
        }
    }
}
