namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantRemovedNotification: Notification, IChatParticipantRemovedNotification
    {
        public ChatParticipantRemovedNotification()
        {
        }

        public ChatParticipantRemovedNotification(Guid initiatorUserId, Guid chatId, IParticipationStatusModificationResult participationStatusModificationResult) : base(initiatorUserId)
        {
            ChatId = chatId;
            ParticipationStatusModificationResult = participationStatusModificationResult;
        }

        public Guid ChatId { get; set; }
        public IParticipationStatusModificationResult ParticipationStatusModificationResult { get; set; }
    }
}
