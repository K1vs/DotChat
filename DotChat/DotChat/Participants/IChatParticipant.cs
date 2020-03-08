namespace K1vs.DotChat.Participants
{
    using K1vs.DotChat.Common;
    using System;

    public interface IChatParticipant: IChatUser, IParticipationCandidate, IVersioned
    {
        ChatParticipantStatus ChatParticipantStatus { get; set; }

        long ReadIndex { get; }
    }
}
