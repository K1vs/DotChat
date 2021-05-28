namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantAppliedNotification: Notification, IChatParticipantAppliedNotification
    {
        public ChatParticipantAppliedNotification()
        {
        }

        public ChatParticipantAppliedNotification(Guid initiatorUserId, Guid chatId, IParticipationModificationResult participationModificationResult) : base(initiatorUserId)
        {
            ChatId = chatId;
            ParticipationModificationResult = participationModificationResult;
        }

        public Guid ChatId { get; set; }
        public IParticipationModificationResult ParticipationModificationResult { get; set; }
    }
}
