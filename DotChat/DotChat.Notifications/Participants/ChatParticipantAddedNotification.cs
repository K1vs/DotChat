namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantAddedNotification: Notification, IChatParticipantAddedNotification
    {
        public ChatParticipantAddedNotification()
        {
        }

        public ChatParticipantAddedNotification(Guid initiatorUserId, Guid chatId, IChatParticipant participant, ChatParticipantStatus? previousStatus) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
            PreviousStatus = previousStatus;
        }

        public Guid ChatId { get; set; }
        public IChatParticipant Participant { get; set; }
        public ChatParticipantStatus? PreviousStatus { get; set; }
    }
}
