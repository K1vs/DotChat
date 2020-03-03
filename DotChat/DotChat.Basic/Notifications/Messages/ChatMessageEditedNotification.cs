namespace K1vs.DotChat.Basic.Notifications.Messages
{
    using System;
    using System.Collections.Generic;
    using Basic.Messages;
    using Basic.Messages.Typed;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Notifications;
    using DotChat.Notifications.Messages;
    using DotChat.Participants;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class ChatMessageEditedNotification: ChatMessageEditedNotification<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatMessageEditedNotification()
        {
        }

        public ChatMessageEditedNotification(Guid initiatorUserId, Guid chatId, ChatMessage message) : base(initiatorUserId, chatId, message)
        {
        }
    }
}
