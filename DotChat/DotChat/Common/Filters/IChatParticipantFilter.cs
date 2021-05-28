namespace K1vs.DotChat.Common.Filters
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Participants;

    public interface IChatParticipantFilter
    {
        string Search { get; }

        Guid? UserId { get; }

        ChatPrivacyMode? ChatPrivacyMode { get; }

        ChatParticipantType? ParticipantType { get; }

        ChatParticipantStatus? ParticipantStatus { get; }
    }
}
