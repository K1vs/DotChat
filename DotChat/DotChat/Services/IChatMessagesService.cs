namespace K1vs.DotChat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Chats;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessagesService
    {
        Task<IPagedResult<IChatMessage>> GetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<IChatMessageFilter> filters, IPagingOptions pagingOptions = default);
        Task<IPagedResult<IChatMessage>> GetPage(Guid currentUserId, Guid chatId, IPagingOptions pagingOptions = default);
        Task<Guid> Add(Guid currentUserId, Guid chatId, Guid? messageId, IChatMessageInfo messageInfo);
        Task<Guid> Edit(Guid currentUserId, Guid chatId, Guid messageId, IChatMessageInfo messageInfo, Guid? archivedMessageId);
        Task Remove(Guid currentUserId, Guid chatId, Guid messageId);
    }
}
