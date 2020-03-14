namespace K1vs.DotChat.Basic.Events.Chat
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using DotChat.Chats;
    using DotChat.Events.Chat;
    using DotChat.Participants;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Participants;

    public class ChatAddedEvent: ChatAddedEvent<Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatAddedEvent()
        {
        }

        public ChatAddedEvent(Guid initiatorUserId, Chat chat) : base(initiatorUserId, chat)
        {
        }
    }
}
