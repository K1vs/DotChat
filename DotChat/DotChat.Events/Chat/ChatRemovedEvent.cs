﻿namespace K1vs.DotChat.Events.Chat
{
    using System;
    using K1vs.DotChat.Chats;

    public class ChatRemovedEvent<TChatInfo>: EventBase, IChatRemovedEvent<TChatInfo>
        where TChatInfo : IChatInfo
    {
        public ChatRemovedEvent()
        {
        }

        public ChatRemovedEvent(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
        }

        public Guid ChatId { get; set; }
        public TChatInfo ChatInfo { get; set; }
    }
}