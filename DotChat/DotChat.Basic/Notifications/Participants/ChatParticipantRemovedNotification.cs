﻿namespace K1vs.DotChat.Basic.Notifications.Participants
{
    using System;
    using DotChat.Notifications;
    using DotChat.Notifications.Participants;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatParticipantRemovedNotification: ChatParticipantRemovedNotification<ChatParticipant>
    {
        public ChatParticipantRemovedNotification()
        {
        }

        public ChatParticipantRemovedNotification(Guid initiatorUserId, Guid chatId, ChatParticipant participant) : base(initiatorUserId, chatId, participant)
        {
        }
    }
}
