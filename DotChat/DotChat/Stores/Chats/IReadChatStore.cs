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
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Messages;

    public interface IReadChatStore
    {
        Task<IPersonalizedChatsSummary> RetrievePersonalizedSummary(Guid userId);
        Task<IReadOnlyCollection<Guid>> RetrieveAllIds(Guid userId);
        Task<IPagedResult<IPersonalizedChat>> RetrievePersonalizedPage(Guid userId, IChatFilter filter, IPagingOptions pagingOptions);
        Task<IPagedResult<IPersonalizedChat>> RetrievePersonalizedPage(Guid userId, IPagingOptions pagingOptions);
        Task<IPersonalizedChat> RetrievePersonalized(Guid chatId, Guid userId);
        Task<IChat> Retrieve(Guid chatId);
    }
}
