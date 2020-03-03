namespace K1vs.DotChat.Basic.Notifications.Participants
{
    using System;
    using DotChat.Notifications;
    using DotChat.Notifications.Participants;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatParticipantInvitedNotification: ChatParticipantInvitedNotification<ChatParticipant> 
    {
        public ChatParticipantInvitedNotification()
        {
        }

        public ChatParticipantInvitedNotification(Guid initiatorUserId, Guid chatId, ChatParticipant participant) : base(initiatorUserId, chatId, participant)
        {
        }
    }
}
