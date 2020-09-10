namespace K1vs.DotChat.Notifications.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public class ChatAddedNotification: Notification, IChatAddedNotification
    {
        public ChatAddedNotification()
        {
        }

        public ChatAddedNotification(Guid initiatorUserId, IPersonalizedChat personalizedChat) : base(initiatorUserId)
        {
            PersonalizedChat = personalizedChat;
        }

        public IPersonalizedChat PersonalizedChat { get; set; }
    }
}
