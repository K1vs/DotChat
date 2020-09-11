namespace K1vs.DotChat.Events.Chat
{
    using System;
    using Chats;
    using DotChat.Chats;

    public class ChatInfoEditedEvent: Event, IChatInfoEditedEvent
    {
        public ChatInfoEditedEvent()
        {
        }

        public ChatInfoEditedEvent(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
        }

        public Guid ChatId { get; set; }
        public IChatInfo ChatInfo { get; set; }
        public long Version { get; set; }
    }
}
