namespace K1vs.DotChat.Basic.Notifiers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Configuration;
    using DotChat.NotificationBuilders;
    using DotChat.Notifiers;
    using Models.Participants;
    using Participants;

    public class ChatParticipantNotifier: ChatParticipantNotifier<ChatNotificationsConfiguration, List<ParticipationResult>, ParticipationResult, ChatParticipant>
    {
        public ChatParticipantNotifier(ChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatParticipantsNotificationBuilder<List<ParticipationResult>, ParticipationResult, ChatParticipant> chatParticipantsNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService, chatParticipantsNotificationBuilder)
        {
        }
    }
}
