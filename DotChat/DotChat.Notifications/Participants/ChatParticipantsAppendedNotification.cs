namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantsAppendedNotification: Notification, IChatParticipantsAppendedNotification
    {
        public ChatParticipantsAppendedNotification()
        {
        }

        public ChatParticipantsAppendedNotification(Guid initiatorUserId, Guid chatId, IReadOnlyCollection<IParticipationResult> added, IReadOnlyCollection<IParticipationResult> invited) : base(initiatorUserId)
        {
            ChatId = chatId;
            Added = added;
            Invited = invited;
        }

        public Guid ChatId { get; set; }
        public IReadOnlyCollection<IParticipationResult> Added { get; set; }
        public IReadOnlyCollection<IParticipationResult> Invited { get; set; }
    }
}
