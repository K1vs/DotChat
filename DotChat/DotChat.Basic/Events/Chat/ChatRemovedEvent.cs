namespace K1vs.DotChat.Basic.Events.Chat
{
    using System;
    using DotChat.Chats;
    using DotChat.Events.Chat;
    using Models.Chats;

    public class ChatRemovedEvent: ChatRemovedEvent<ChatInfo>
    {
        public ChatRemovedEvent()
        {
        }

        public ChatRemovedEvent(Guid initiatorUserId, Guid chatId, ChatInfo chatInfo) : base(initiatorUserId, chatId, chatInfo)
        {
        }
    }
}
