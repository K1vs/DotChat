namespace K1vs.DotChat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Chats;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using Participants;

    public interface IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, in TChatInfo, TChatParticipantCollection, TChatParticipant, in TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, in TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, in TPagingOptions>
        where TPersonalizedChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection: IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChat: IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo: IChatInfo
        where TChatParticipantCollection: IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidates : IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult: IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions: IPagingOptions
    {
        Task<TPersonalizedChatsSummary> GetSummary(Guid currentUserId);
        Task<TPagedResult> GetPage(Guid currentUserId, TChatFilter filter, TPagingOptions pagingOptions = default);
        Task<TPagedResult> GetPage(Guid currentUserId, TPagingOptions pagingOptions = default);
        Task<TPersonalizedChat> Get(Guid currentUserId, Guid chatId);
        Task<Guid> Add(Guid currentUserId, TChatInfo chatInfo, TParticipationCandidates participationCandidates);
        Task EditInfo(Guid currentUserId, Guid chatId, TChatInfo chatInfo);
        Task Remove(Guid currentUserId, Guid chatId);
    }
}
