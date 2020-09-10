namespace K1vs.DotChat.Models.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public class PersonalizedChat: Chat, IPersonalizedChat
    {
        public PersonalizedChat()
        {
        }

        public PersonalizedChat(string name, string description, ChatPrivacyMode privacyMode, long version, Guid chatId, IReadOnlyCollection<IChatParticipant> participants, DateTime lastTimestamp, long topIndex, Guid? lastMessageId, Guid? lastMessageAuthorId, IChatMessageInfo lastChatMessageInfo, long readIndex, long unreadCount)
            : base(name, description, privacyMode, version, chatId, participants, lastTimestamp, topIndex, lastMessageId, lastMessageAuthorId, lastChatMessageInfo)
        {
            ReadIndex = readIndex;
            UnreadCount = unreadCount;
        }

        public long ReadIndex { get; set; }
        public long UnreadCount { get; set; }
    }
}
