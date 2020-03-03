﻿namespace K1vs.DotChat.Basic.Notifications.Chats
{
    using System;
    using System.Collections.Generic;
    using Basic.Chats;
    using DotChat.Chats;
    using DotChat.Notifications;
    using DotChat.Notifications.Chats;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatAddedNotification: ChatAddedNotification<PersonalizedChat, List<ChatParticipant>, ChatParticipant>
    {
        public ChatAddedNotification()
        {
        }

        public ChatAddedNotification(Guid initiatorUserId, PersonalizedChat personalizedChat) : base(initiatorUserId, personalizedChat)
        {
        }
    }
}
