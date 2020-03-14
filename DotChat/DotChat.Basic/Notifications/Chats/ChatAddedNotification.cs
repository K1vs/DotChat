namespace K1vs.DotChat.Basic.Notifications.Chats
{
    using System;
    using System.Collections.Generic;
    using Basic.Chats;
    using DotChat.Chats;
    using DotChat.Notifications;
    using DotChat.Notifications.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Participants;

    public class ChatAddedNotification: ChatAddedNotification<PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatAddedNotification()
        {
        }

        public ChatAddedNotification(Guid initiatorUserId, PersonalizedChat personalizedChat) : base(initiatorUserId, personalizedChat)
        {
        }
    }
}
