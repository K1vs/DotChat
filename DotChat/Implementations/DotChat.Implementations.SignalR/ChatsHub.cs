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

    public abstract class ChatsHub<TChatsClient, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo,
            TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, 
            TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions>
        : Hub<TChatsClient>
        where TChatsClient: class, IChatsClient<TPersonalizedChat, TChatInfo, TChatParticipantCollection, TChatParticipant>
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

        public async Task<TPersonalizedChatsSummary> GetSummary()
        {
            return await _chatsService.GetSummary(CurrentUserId);
        }

        public async Task<TPagedResult> GetPage(TChatFilter filter, TPagingOptions pagingOptions = default)
        {
            return await _chatsService.GetPage(CurrentUserId, filter, pagingOptions);
        }

        public async Task<TPagedResult> GetPage(TPagingOptions pagingOptions = default)
        {
            return await _chatsService.GetPage(CurrentUserId, pagingOptions);
        }

        public async Task<TPersonalizedChat> Get(Guid chatId)
        {
            return await _chatsService.Get(CurrentUserId, chatId);
        }

        public async Task<Guid> Add(TChatInfo chatInfo, TParticipationCandidates participationCandidates)
        {
            return await _chatsService.Add(CurrentUserId, chatInfo, participationCandidates);
        }

        public async Task EditInfo(Guid chatId, TChatInfo chatInfo)
        {
            await _chatsService.EditInfo(CurrentUserId, chatId, chatInfo);
        }

        public async Task Remove(Guid chatId)
        {
            await _chatsService.Remove(CurrentUserId, chatId);
        }

        protected virtual Guid CurrentUserId => Guid.Parse(Context.User.Identity.Name);
    }
}
