namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantsAppendedNotification<TParticipationResultCollection, TParticipationResult, TChatParticipant> : NotificationBase, IChatParticipantsAppendedNotification<TParticipationResultCollection, TParticipationResult, TChatParticipant>
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatParticipantsAppendedNotification()
        {
        }

        public ChatParticipantsAppendedNotification(Guid initiatorUserId, Guid chatId, TParticipationResultCollection added, TParticipationResultCollection invited) : base(initiatorUserId)
        {
            ChatId = chatId;
            Added = added;
            Invited = invited;
        }

        public Guid ChatId { get; set; }
        public TParticipationResultCollection Added { get; set; }
        public TParticipationResultCollection Invited { get; set; }
    }
}
