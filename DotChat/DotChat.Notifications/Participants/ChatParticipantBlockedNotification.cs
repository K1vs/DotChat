namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantBlockedNotification: Notification, IChatParticipantBlockedNotification
    {
        public ChatParticipantBlockedNotification()
        {
        }

        public ChatParticipantBlockedNotification(Guid initiatorUserId, Guid chatId, IChatParticipant participant) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
        }

        public Guid ChatId { get; set; }
        public IChatParticipant Participant { get; set; }
    }
}
