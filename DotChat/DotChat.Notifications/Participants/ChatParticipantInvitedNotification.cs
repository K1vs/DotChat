namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public class ChatParticipantInvitedNotification: Notification, IChatParticipantInvitedNotification
    {
        public ChatParticipantInvitedNotification()
        {
        }

        public ChatParticipantInvitedNotification(Guid initiatorUserId, Guid chatId, IChatParticipant participant) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
        }

        public Guid ChatId { get; set; }
        public IChatParticipant Participant { get; set; }
    }
}
