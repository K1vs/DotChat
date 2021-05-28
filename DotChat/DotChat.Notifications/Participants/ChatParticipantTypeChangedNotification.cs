namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Participants;

    public class ChatParticipantTypeChangedNotification : Notification, IChatParticipantTypeChangedNotification
    {
        public ChatParticipantTypeChangedNotification()
        {
        }

        public ChatParticipantTypeChangedNotification(Guid initiatorUserId, Guid chatId, IParticipationTypeModificationResult participationTypeModificationResult) : base(initiatorUserId)
        {
            ChatId = chatId;
            ParticipationTypeModificationResult = participationTypeModificationResult;
        }

        public Guid ChatId { get; set; }
        public IParticipationTypeModificationResult ParticipationTypeModificationResult { get; set; }
    }
}
