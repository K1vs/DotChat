namespace K1vs.DotChat.Models.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;

    public class PersonalizedChat<TChatParticipantCollection, TChatParticipant> : Chat<TChatParticipantCollection, TChatParticipant>, IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public PersonalizedChat()
        {
        }

        public PersonalizedChat(string name, string description, ChatPrivacyMode privacyMode, Guid chatId, TChatParticipantCollection participants, DateTime lastTimestamp, long topIndex, long readIndex, long unreadCount) : base(name, description, privacyMode, chatId, participants, lastTimestamp, topIndex)
        {
            ReadIndex = readIndex;
            UnreadCount = unreadCount;
        }

        public long ReadIndex { get; set; }
        public long UnreadCount { get; set; }
    }
}
