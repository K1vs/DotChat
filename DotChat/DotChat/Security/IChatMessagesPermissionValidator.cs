namespace K1vs.DotChat.Security
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Chats;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessagesPermissionValidator
    {
        Task ValidateGetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<IChatMessageFilter> filters, IPagingOptions pagingOptions, IPagedResult<IChatMessage> messagesPage);
        Task ValidateGetPage(Guid currentUserId, Guid chatId, IPagingOptions pagingOptions, IPagedResult<IChatMessage> messagesPage);
        Task ValidateAdd(Guid currentUserId, Guid chatId, IChatMessageInfo messageInfo);
        Task ValidateEdit(Guid currentUserId, Guid chatId, Guid messageId, IChatMessageInfo newMessage);
        Task ValidateRemove(Guid currentUserId, Guid chatId, Guid messageId);
    }
}
