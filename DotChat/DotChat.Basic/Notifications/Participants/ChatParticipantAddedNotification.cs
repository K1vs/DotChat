namespace K1vs.DotChat.Basic.Notifications.Participants
{
    using System;
    using DotChat.Notifications;
    using DotChat.Notifications.Participants;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatParticipantAddedNotification: ChatParticipantAddedNotification<ChatParticipant>
    {
        public ChatParticipantAddedNotification()
        {
        }

        public ChatParticipantAddedNotification(Guid initiatorUserId, Guid chatId, ChatParticipant participant, ChatParticipantStatus? previousStatus) : base(initiatorUserId, chatId, participant, previousStatus)
        {
        }
    }
}
