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

    public class Chat : Chat<ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public Chat()
        {
        }

        public Chat(string name, string description, ChatPrivacyMode privacyMode, long version, Guid chatId, List<ChatParticipant> participants, DateTime lastTimestamp, long topIndex, Guid? lastMessageId, Guid? lastMessageAuthorId, ChatMessageInfo lastChatMessageInfo) 
            : base(name, description, privacyMode, version, chatId, participants, lastTimestamp, topIndex, lastMessageId, lastMessageAuthorId, lastChatMessageInfo)
        {
        }

        public Chat(IChatInfo chatInfo, Guid chatId, List<ChatParticipant> participants, DateTime lastTimestamp, long topIndex, Guid? lastMessageId, Guid? lastMessageAuthorId, ChatMessageInfo lastChatMessageInfo)
            : base(chatInfo, chatId, participants, lastTimestamp, topIndex, lastMessageId, lastMessageAuthorId, lastChatMessageInfo)
        {
        }
    }
}
