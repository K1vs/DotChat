namespace K1vs.DotChat.Stores.Chats
{
    using K1vs.DotChat.Chats;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Participants;

    public interface IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatParticipantCollection, TChatParticipant, in TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, in TPagingOptions>
        where TPersonalizedChatsSummary: IPersonalizedChatsSummary
        where TPersonalizedChatCollection: IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        Task<TPersonalizedChatsSummary> RetrievePersonalizedSummary(Guid userId);
        Task<IReadOnlyCollection<Guid>> RetrieveAllIds(Guid userId);
        Task<TPagedResult> RetrievePersonalizedPage(Guid userId, TChatFilter filter, TPagingOptions pagingOptions);
        Task<TPagedResult> RetrievePersonalizedPage(Guid userId, TPagingOptions pagingOptions);
        Task<TPersonalizedChat> RetrievePersonalized(Guid chatId, Guid userId);
        Task<TChat> Retrieve(Guid chatId);
    }
}
