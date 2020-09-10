namespace K1vs.DotChat.Stores.Messages
{
    using System;
    using System.Collections.Generic;
    using K1vs.DotChat.Messages;
    using System.Threading.Tasks;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Chats;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public interface IReadChatMessageStore
    {
        Task<IPagedResult<IChatMessage>> Retrieve(Guid chatId, IReadOnlyCollection<IMessageFilter> filters, IPagingOptions options);
        Task<IPagedResult<IChatMessage>> Retrieve(Guid chatId, IPagingOptions options);
        Task<IChatMessage> Retrieve(Guid chatId, Guid messageId);
    }
}
