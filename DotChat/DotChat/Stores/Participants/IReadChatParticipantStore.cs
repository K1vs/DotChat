namespace K1vs.DotChat.Stores.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Participants;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;

    public interface IReadChatParticipantStore
    {
        Task<IPagedResult<IChatParticipant>> Retrieve(Guid chatId, IReadOnlyCollection<IChatParticipantFilter> filters, IPagingOptions options);
        Task<IPagedResult<IChatParticipant>> Retrieve(Guid chatId, IPagingOptions options);
        Task<IChatParticipant> Retrieve(Guid chatId, Guid userId);
        Task<IReadOnlyCollection<Guid>> RetrieveIds(Guid chatId);
    }
}
