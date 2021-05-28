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
        Task<IChatsSummary> RetrieveSummary();
        Task<IPagedResult<IChat>> RetrievePage(Guid userId, IChatFilter filter, IPagingOptions pagingOptions);
        Task<IPagedResult<IChat>> RetrievePage(Guid userId, IPagingOptions pagingOptions);
        Task<IChat> Retrieve(Guid chatId);
        Task<IReadOnlyCollection<Guid>> RetrieveIds(Guid userId);
    }
}
