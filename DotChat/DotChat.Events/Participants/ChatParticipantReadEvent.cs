namespace K1vs.DotChat.Events.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Messages;
    using K1vs.DotChat.Events.Participants;

    public class ChatParticipantReadEvent: Event, IChatParticipantReadEvent
    {
        public ChatParticipantReadEvent()
        {
        }

        public ChatParticipantReadEvent(Guid initiatorUserId, Guid chatId, long index, bool force) : base(initiatorUserId)
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
