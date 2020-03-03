namespace K1vs.DotChat.Stores.Chats
{
    using K1vs.DotChat.Chats;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Participants;

    public interface IChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, in TParticipationCandidateCollection, TParticipationCandidate, in TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, in TPagingOptions> : 
        IReadChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions>
        where TChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection : IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo: IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        Task<TChat> Create(Guid chatId, TChatInfo chatInfo, TParticipationCandidateCollection toAdd, TParticipationCandidateCollection toInvite, Guid creatorId);
        Task<TChatInfo> UpdateInfo(Guid chatId, TChatInfo chatInfo, Guid modifierId);
        Task<TChatInfo> Delete(Guid chatId, Guid removerId);
        Task SetTop(Guid chatId, DateTime lastTimestamp, long topIndex);
    }
}
