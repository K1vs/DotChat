namespace K1vs.DotChat.Basic.Notifications.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Notifications.Participants;
    using Models.Participants;

    public class ChatParticipantTypeChangedNotification: ChatParticipantTypeChangedNotification<ChatParticipant>
    {
        public ChatParticipantTypeChangedNotification()
        {
        }

        public ChatParticipantTypeChangedNotification(Guid initiatorUserId, Guid chatId, ChatParticipant participant) : base(initiatorUserId, chatId, participant)
        {
        }
    }
}
