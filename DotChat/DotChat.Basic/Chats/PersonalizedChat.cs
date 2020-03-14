namespace K1vs.DotChat.Basic.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Chats;
    using Models.Participants;

    public class PersonalizedChat: PersonalizedChat<ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public PersonalizedChat()
        {
        }

        public PersonalizedChat(string name, string description, ChatPrivacyMode privacyMode, long version, Guid chatId, List<ChatParticipant> participants, DateTime lastTimestamp, long topIndex, long readIndex, long unreadCount) : base(name, description, privacyMode, version, chatId, participants, lastTimestamp, topIndex, readIndex, unreadCount)
        {
        }
    }
}
