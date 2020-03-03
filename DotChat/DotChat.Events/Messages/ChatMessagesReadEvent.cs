namespace K1vs.DotChat.Events.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Messages;

    public class ChatMessagesReadEvent: EventBase, IChatMessagesReadEvent
    {
        public ChatMessagesReadEvent()
        {
        }

        public ChatMessagesReadEvent(Guid initiatorUserId, Guid chatId, long index) : base(initiatorUserId)
        {
            ChatId = chatId;
            Index = index;
        }

        public Guid ChatId { get; set; }
        public long Index { get; set; }
    }
}
