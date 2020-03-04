namespace K1vs.DotChat.Implementations.SignalR
{
    using K1vs.DotChat.Chats;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Configuration;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;
    using K1vs.DotChat.Services;
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ChatsHub<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo,
            TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, 
            TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions>
        : Hub, IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat,
            TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection,
            TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions>
        where TPersonalizedChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection : IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidates : IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        private readonly IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat,
            TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates,
            TParticipationCandidateCollection, TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter
            , TPagedResult, TPagingOptions> _chatsService;

        public ChatsHub(IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo,
                TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, 
                TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> chatsService)
        {
            _chatsService = chatsService;
        }

        public async Task<TPersonalizedChatsSummary> GetSummary(Guid currentUserId)
        {
            return await _chatsService.GetSummary(currentUserId);
        }

        public async Task<TPagedResult> GetPage(Guid currentUserId, TChatFilter filter, TPagingOptions pagingOptions = default)
        {
            return await _chatsService.GetPage(currentUserId, filter, pagingOptions);
        }

        public async Task<TPagedResult> GetPage(Guid currentUserId, TPagingOptions pagingOptions = default)
        {
            return await _chatsService.GetPage(currentUserId, pagingOptions);
        }

        public async Task<TPersonalizedChat> Get(Guid currentUserId, Guid chatId)
        {
            return await _chatsService.Get(currentUserId, chatId);
        }

        public async Task<Guid> Add(Guid currentUserId, TChatInfo chatInfo, TParticipationCandidates participationCandidates)
        {
            return await _chatsService.Add(currentUserId, chatInfo, participationCandidates);
        }

        public async Task Edit(Guid currentUserId, Guid chatId, TChatInfo chatInfo)
        {
            await _chatsService.Edit(currentUserId, chatId, chatInfo);
        }

        public async Task Remove(Guid currentUserId, Guid chatId)
        {
            await _chatsService.Remove(currentUserId, chatId);
        }
    }
}
