namespace K1vs.DotChat.Events.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Messages;

    public class ChatMessagesReadEvent: Event, IChatMessagesReadEvent
    {
        public ChatMessagesReadEvent()
        {
        }

        public ChatMessagesReadEvent(Guid initiatorUserId, Guid chatId, long index, bool force) : base(initiatorUserId)
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
