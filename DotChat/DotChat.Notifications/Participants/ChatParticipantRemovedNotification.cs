namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantRemovedNotification<TChatParticipant>: NotificationBase, IChatParticipantRemovedNotification<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatParticipantRemovedNotification()
        {
        }

        public ChatParticipantRemovedNotification(Guid initiatorUserId, Guid chatId, TChatParticipant participant) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
        }

        public Guid ChatId { get; set; }
        public TChatParticipant Participant { get; set; }
    }
}
