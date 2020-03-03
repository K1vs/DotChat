namespace K1vs.DotChat.Common.Filters
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Participants;

    public class ChatUserFilter: IChatUserFilter
    {
        public ChatUserFilter()
        {
        }

        public ChatUserFilter(Guid? userId, ChatPrivacyMode? chatPrivacyMode, ChatParticipantType? participantType, ChatParticipantStatus? participantStatus)
        {
            UserId = userId;
            ChatPrivacyMode = chatPrivacyMode;
            ParticipantType = participantType;
            ParticipantStatus = participantStatus;
        }

        public Guid? UserId { get; set; }
        public ChatPrivacyMode? ChatPrivacyMode { get; set; }
        public ChatParticipantType? ParticipantType { get; set; }
        public ChatParticipantStatus? ParticipantStatus { get; set; }
    }
}
