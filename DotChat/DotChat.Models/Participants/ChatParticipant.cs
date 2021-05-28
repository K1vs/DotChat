namespace K1vs.DotChat.Models.Participants
{
    using System;
    using System.Collections.Generic;
    using DotChat.Participants;
    using K1vs.DotChat.Models.Users;
    using K1vs.DotChat.Users;

    public class ChatParticipant: IChatParticipant
    {
        public ChatParticipant()
        {
        }

        public ChatParticipant(ChatParticipantType chatParticipantType, ChatParticipantStatus chatParticipantStatus, DateTime readTimestamp, long readIndex, long version, IChatUser user)
        {
            ChatParticipantType = chatParticipantType;
            ChatParticipantStatus = chatParticipantStatus;
            ReadTimestamp = readTimestamp;
            ReadIndex = readIndex;
            Version = version;
            UserId = user.UserId;
            ChatUser = user;
        }

        public ChatParticipantType ChatParticipantType { get; set; }
        public ChatParticipantStatus ChatParticipantStatus { get; set; }
        public DateTime ReadTimestamp { get; set; }
        public long ReadIndex { get; set; }
        public long Version { get; set; }
        public IChatUser ChatUser { get; set; }
        public Guid UserId { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}
