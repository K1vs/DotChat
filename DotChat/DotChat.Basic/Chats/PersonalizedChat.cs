namespace K1vs.DotChat.Basic.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Models.Chats;
    using Models.Participants;

    public class PersonalizedChat: PersonalizedChat<List<ChatParticipant>, ChatParticipant>
    {
        public PersonalizedChat()
        {
        }

        public PersonalizedChat(string name, string description, ChatPrivacyMode privacyMode, Guid chatId, List<ChatParticipant> participants, DateTime lastTimestamp, long topIndex, long readIndex, long unreadCount) : base(name, description, privacyMode, chatId, participants, lastTimestamp, topIndex, readIndex, unreadCount)
        {
        }
    }
}
