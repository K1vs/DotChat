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
        Task ValidateGetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<IMessageFilter> filters, IPagingOptions pagingOptions, IPagedResult<IChatMessage> messagesPage, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateGetPage(Guid currentUserId, Guid chatId, IPagingOptions pagingOptions, IPagedResult<IChatMessage> messagesPage, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateRead(Guid currentUserId, Guid chatId, long index, bool force, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateAdd(Guid currentUserId, Guid chatId, IChatMessageInfo messageInfo, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateRemove(Guid currentUserId, Guid chatId, Guid messageId, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateEdit(Guid currentUserId, Guid chatId, Guid messageId, IChatMessageInfo newMessage, string serviceName, [CallerMemberName] string methodName = null);
    }
}
