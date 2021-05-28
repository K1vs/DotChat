namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantsAppendedNotification: Notification, IChatParticipantBulkAddedInvitedNotification
    {
        public ChatParticipantsAppendedNotification()
        {
        }

        public ChatParticipantsAppendedNotification(Guid initiatorUserId, Guid chatId, IReadOnlyCollection<IParticipationModificationResult> added, IReadOnlyCollection<IParticipationModificationResult> invited) : base(initiatorUserId)
        {
            ChatId = chatId;
            Added = added;
            Invited = invited;
        }

        public Guid ChatId { get; set; }
        public IReadOnlyCollection<IParticipationModificationResult> Added { get; set; }
        public IReadOnlyCollection<IParticipationModificationResult> Invited { get; set; }
    }
}
