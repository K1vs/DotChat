namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantAddedNotification<TChatParticipant>: Notification, IChatParticipantAddedNotification<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatParticipantAddedNotification()
        {
        }

        public ChatParticipantAddedNotification(Guid initiatorUserId, Guid chatId, TChatParticipant participant, ChatParticipantStatus? previousStatus) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
            PreviousStatus = previousStatus;
        }

        public Guid ChatId { get; set; }
        public TChatParticipant Participant { get; set; }
        public ChatParticipantStatus? PreviousStatus { get; set; }
    }
}
