namespace K1vs.DotChat.Basic.Notifiers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Configuration;
    using DotChat.Notifiers;
    using Models.Participants;
    using Stores.Participants;

    public class NotificationRouteService: NotificationRouteService<ChatNotificationsConfiguration, ChatParticipant>
    {
        public NotificationRouteService(IReadChatParticipantStore<ChatParticipant> readChatParticipantStore, ChatNotificationsConfiguration notificationsConfiguration) : base(readChatParticipantStore, notificationsConfiguration)
        {
        }
    }
}
