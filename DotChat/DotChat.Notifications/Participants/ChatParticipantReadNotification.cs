namespace K1vs.DotChat.Notifications.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Messages;
    using Events;
    using K1vs.DotChat.Notifications.Participants;

    public class ChatParticipantReadNotification: Notification, IChatParticipantReadNotification
    {
        public ChatParticipantReadNotification()
        {
        }

        public ChatParticipantReadNotification(Guid initiatorUserId, Guid chatId, long index, bool force) : base(initiatorUserId)
        {
            ChatId = chatId;
            Index = index;
            Force = force;
        }

        public Guid ChatId { get; set; }
        public long Index { get; set; }
        public bool Force { get; set; }
    }
}
