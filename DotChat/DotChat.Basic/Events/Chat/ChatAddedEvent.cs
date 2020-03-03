namespace K1vs.DotChat.Basic.Events.Chat
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using DotChat.Chats;
    using DotChat.Events.Chat;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatAddedEvent: ChatAddedEvent<Chat, List<ChatParticipant>, ChatParticipant>
    {
        public ChatAddedEvent()
        {
        }

        public ChatAddedEvent(Guid initiatorUserId, Chat chat) : base(initiatorUserId, chat)
        {
        }
    }
}
