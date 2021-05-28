namespace K1vs.DotChat.Models.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Users;

    public class Chat: IChat
    {
        public Chat()
        {
        }

        public Chat(IChatInfo chatInfo, Guid chatId, DateTime lastTimestamp, long topIndex, Guid? lastMessageId, long lastMessageIndex, Guid? lastMessageAuthorId, IChatMessageInfo lastChatMessageInfo, IReadOnlyCollection<IChatParticipant> topParticipants)
        {
            ChatId = chatId;
            LastTimestamp = lastTimestamp;
            TopIndex = topIndex;
            LastMessageId = lastMessageId;
            LastMessageAuthorId = lastMessageAuthorId;
            LastMessageIndex = lastMessageIndex;
            LastChatMessageInfo = lastChatMessageInfo;
            ChatInfo = chatInfo;
            Version = chatInfo.Version;
            TopParticipants = topParticipants;
        }

        public Guid ChatId { get; set; }

        public DateTime LastTimestamp { get; set; }

        public long TopIndex { get; set; }

        public Guid? LastMessageId { get; set; }

        public Guid? LastMessageAuthorId { get; set; }

        public long LastMessageIndex { get; set; }

        public IChatMessageInfo LastChatMessageInfo { get; set; }

        public IChatUser LastMessageAuthorInfo { get; set; }

        public long PaticipantsCount { get; set; }

        public IChatInfo ChatInfo { get; set; }

        public long Version { get; set; }

        public IReadOnlyCollection<IChatParticipant> TopParticipants { get; set; }
    }
}
