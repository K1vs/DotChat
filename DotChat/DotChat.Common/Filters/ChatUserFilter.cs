namespace K1vs.DotChat.Common.Filters
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Participants;

    public class ChatUserFilter: IChatParticipantFilter
    {
        public ChatUserFilter()
        {
        }

        public ChatUserFilter(string search, Guid? userId, ChatPrivacyMode? chatPrivacyMode, ChatParticipantType? participantType, ChatParticipantStatus? participantStatus)
        {
            Search = search;
            UserId = userId;
            ChatPrivacyMode = chatPrivacyMode;
            ParticipantType = participantType;
            ParticipantStatus = participantStatus;
        }

        public string Search { get; }
        public Guid? UserId { get; set; }
        public ChatPrivacyMode? ChatPrivacyMode { get; set; }
        public ChatParticipantType? ParticipantType { get; set; }
        public ChatParticipantStatus? ParticipantStatus { get; set; }
    }
}
