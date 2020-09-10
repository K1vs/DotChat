namespace K1vs.DotChat.Events.Chat
{
    using System;
    using Chats;
    using K1vs.DotChat.Chats;

    public class ChatRemovedEvent: Event, IChatRemovedEvent
    {
        public ChatRemovedEvent()
        {
        }

        public ChatRemovedEvent(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
        }

        public Guid ChatId { get; set; }
        public IChatInfo ChatInfo { get; set; }
        public long Version { get; set; }
    }
}
