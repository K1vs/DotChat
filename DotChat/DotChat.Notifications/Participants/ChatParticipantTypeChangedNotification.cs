namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Participants;

    public class ChatParticipantTypeChangedNotification<TChatParticipant> : Notification, IChatParticipantTypeChangedNotification<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatParticipantTypeChangedNotification()
        {
        }

        public ChatParticipantTypeChangedNotification(Guid initiatorUserId, Guid chatId, TChatParticipant participant) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
        }

        public Guid ChatId { get; set; }
        public TChatParticipant Participant { get; set; }
    }
}
