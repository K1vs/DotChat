namespace K1vs.DotChat.Events.Chat
{
    using System;
    using Chats;
    using DotChat.Chats;

    public class ChatInfoEditedEvent<TChatInfo>: Event, IChatInfoEditedEvent<TChatInfo>
        where TChatInfo : IChatInfo
    {
        public ChatInfoEditedEvent()
        {
        }

        public ChatInfoEditedEvent(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
        }

        public Guid ChatId { get; set; }
        public TChatInfo ChatInfo { get; set; }
    }
}
