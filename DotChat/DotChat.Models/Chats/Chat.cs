namespace K1vs.DotChat.Models.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;

    public class Chat<TChatParticipantCollection, TChatParticipant> : ChatInfo, IChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public Chat()
        {
        }

        public Chat(string name, string description, ChatPrivacyMode privacyMode, long version, Guid chatId, TChatParticipantCollection participants, DateTime lastTimestamp, long topIndex, string style = null, string metadata = null) 
            : base(name, description, privacyMode, version, style, metadata)
        {
            ChatId = chatId;
            Participants = participants;
            LastTimestamp = lastTimestamp;
            TopIndex = topIndex;
        }

        public Chat(IChatInfo chatInfo, Guid chatId, TChatParticipantCollection participants, DateTime lastTimestamp, long topIndex) 
            : this(chatInfo.Name, chatInfo.Description, chatInfo.PrivacyMode, chatInfo.Version, chatId, participants, lastTimestamp, topIndex, chatInfo.Style, chatInfo.Metadata) { }

        public Guid ChatId { get; set; }
        public TChatParticipantCollection Participants { get; set; }
        public DateTime LastTimestamp { get; set; }
        public long TopIndex { get; set; }
}
}
