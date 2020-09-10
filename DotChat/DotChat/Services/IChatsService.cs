namespace K1vs.DotChat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Chats;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IChatsService
    {
        Task<IPersonalizedChatsSummary> GetSummary(Guid currentUserId);
        Task<IPagedResult<IPersonalizedChatsSummary>> GetPage(Guid currentUserId, IChatFilter filter, IPagingOptions pagingOptions = default);
        Task<IPagedResult<IPersonalizedChatsSummary>> GetPage(Guid currentUserId, IPagingOptions pagingOptions = default);
        Task<IPersonalizedChat> Get(Guid currentUserId, Guid chatId);
        Task<Guid> Add(Guid currentUserId, Guid? chatId, IChatInfo chatInfo, IHasParticipationCandidates participationCandidates);
        Task EditInfo(Guid currentUserId, Guid chatId, IChatInfo chatInfo);
        Task Remove(Guid currentUserId, Guid chatId);
    }
}
