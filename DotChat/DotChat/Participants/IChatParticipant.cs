namespace K1vs.DotChat.Participants
{
    using System;

    public interface IChatParticipant: IChatUser, IParticipationCandidate
    {
        ChatParticipantStatus ChatParticipantStatus { get; set; }

        long ReadIndex { get; }
    }
}
