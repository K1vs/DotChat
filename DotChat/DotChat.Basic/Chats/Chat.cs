namespace K1vs.DotChat.Basic.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Models.Chats;
    using Models.Participants;

    public class Chat : Chat<List<ChatParticipant>, ChatParticipant>
    {
        public Chat()
        {
        }

        public Chat(string name, string description, ChatPrivacyMode privacyMode, long version, Guid chatId, List<ChatParticipant> participants, DateTime lastTimestamp, long topIndex) 
            : base(name, description, privacyMode, version, chatId, participants, lastTimestamp, topIndex)
        {
        }

        public Chat(IChatInfo chatInfo, Guid chatId, List<ChatParticipant> participants, DateTime lastTimestamp, long topIndex) : base(chatInfo, chatId, participants, lastTimestamp, topIndex)
        {
        }
    }
}
