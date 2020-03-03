namespace K1vs.DotChat.Basic.Notifications.Participants
{
    using System;
    using System.Collections.Generic;
    using Basic.Participants;
    using DotChat.Notifications;
    using DotChat.Notifications.Participants;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatParticipantsAppendedNotification: ChatParticipantsAppendedNotification<List<ParticipationResult>, ParticipationResult, ChatParticipant>
    {
        public ChatParticipantsAppendedNotification()
        {
        }

        public ChatParticipantsAppendedNotification(Guid initiatorUserId, Guid chatId, List<ParticipationResult> added, List<ParticipationResult> invited) : base(initiatorUserId, chatId, added, invited)
        {
        }
    }
}
