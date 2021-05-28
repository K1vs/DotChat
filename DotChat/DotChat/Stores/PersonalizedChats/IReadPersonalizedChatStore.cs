namespace K1vs.DotChat.Stores.PersonalizedChats
{
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.PersonalizedChats;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReadPersonalizedChatStore
    {
        Task<IPersonalizedChatsSummary> RetrieveSummary();
        Task<IPersonalizedChat> RetrievePersonalized(Guid chatId, Guid userId);
        Task<IPagedResult<IPersonalizedChat>> RetrievePage(Guid userId, IChatFilter filter, IPagingOptions pagingOptions);
        Task<IPagedResult<IPersonalizedChat>> RetrievePage(Guid userId, IPagingOptions pagingOptions);
    }
}
