namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantAppliedNotification<TChatParticipant>: Notification, IChatParticipantAppliedNotification<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatParticipantAppliedNotification()
        {
        }

        public ChatParticipantAppliedNotification(Guid initiatorUserId, Guid chatId, TChatParticipant participant) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
        }

        public Guid ChatId { get; set; }
        public TChatParticipant Participant { get; set; }
    }
}
