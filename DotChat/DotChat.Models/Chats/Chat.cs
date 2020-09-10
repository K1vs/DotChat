namespace K1vs.DotChat.Models.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public class Chat: ChatInfo, IChat
    {
        public Chat()
        {
        }

        public Chat(string name, string description, ChatPrivacyMode privacyMode, long version, Guid chatId, IReadOnlyCollection<IChatParticipant> participants, DateTime lastTimestamp, long topIndex, Guid? lastMessageId, Guid? lastMessageAuthorId, IChatMessageInfo lastChatMessageInfo, string style = null, string metadata = null) 
            : base(name, description, privacyMode, version, style, metadata)
        {
            ChatId = chatId;
            Participants = participants;
            LastTimestamp = lastTimestamp;
            TopIndex = topIndex;
            LastMessageId = lastMessageId;
            LastMessageAuthorId = lastMessageAuthorId;
            LastChatMessageInfo = lastChatMessageInfo;
        }

        public Chat(IChatInfo chatInfo, Guid chatId, IReadOnlyCollection<IChatParticipant> participants, DateTime lastTimestamp, long topIndex, Guid? lastMessageId, Guid? lastMessageAuthorId, IChatMessageInfo lastChatMessageInfo) 
            : this(chatInfo.Name, chatInfo.Description, chatInfo.PrivacyMode, chatInfo.Version, chatId, participants, lastTimestamp, topIndex, lastMessageId, lastMessageAuthorId, lastChatMessageInfo, chatInfo.Style, chatInfo.Metadata) { }

        public Guid ChatId { get; set; }

        public IReadOnlyCollection<IChatParticipant> Participants { get; set; }

        public DateTime LastTimestamp { get; set; }

        public long TopIndex { get; set; }

        public Guid? LastMessageId { get; set; }

        public Guid? LastMessageAuthorId { get; set; }

        public IChatMessageInfo LastChatMessageInfo { get; set; }
    }
}
