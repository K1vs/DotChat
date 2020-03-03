namespace K1vs.DotChat.Basic.Events.Messages
{
    using System;
    using System.Collections.Generic;
    using Basic.Messages;
    using Basic.Messages.Typed;
    using DotChat.Chats;
    using DotChat.Events.Messages;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class ChatMessageEditedEvent: ChatMessageEditedEvent<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatMessageEditedEvent()
        {
        }

        public ChatMessageEditedEvent(Guid initiatorUserId, Guid chatId, ChatMessage message) : base(initiatorUserId, chatId, message)
        {
        }
    }
}
