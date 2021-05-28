namespace K1vs.DotChat.Participants
{
    using K1vs.DotChat.Common;
    using K1vs.DotChat.Users;
    using System;

    public interface IChatParticipant: IParticipationCandidate, IVersioned
    {
        IChatUser ChatUser { get; set; }

        ChatParticipantStatus ChatParticipantStatus { get; set; }

        long ReadIndex { get; }
    }
}
