namespace K1vs.DotChat.Commands.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IAddChatCommand<out TChatInfo, out TParticipationCandidateCollection, out TParticipationCandidate> : ICommand, IChatRelated, IHasChatInfo<TChatInfo>, IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TChatInfo: IChatInfo
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
    }
}
