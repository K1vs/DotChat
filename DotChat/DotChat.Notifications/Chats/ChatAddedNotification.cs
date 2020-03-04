namespace K1vs.DotChat.Notifications.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;
    using Participants;

    public class ChatAddedNotification<TPersonalizedChat, TChatParticipantCollection, TChatParticipant>: Notification, IChatAddedNotification<TPersonalizedChat, TChatParticipantCollection, TChatParticipant>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatAddedNotification()
        {
        }

        public ChatAddedNotification(Guid initiatorUserId, TPersonalizedChat personalizedChat) : base(initiatorUserId)
        {
            PersonalizedChat = personalizedChat;
        }

        public TPersonalizedChat PersonalizedChat { get; set; }
    }
}
