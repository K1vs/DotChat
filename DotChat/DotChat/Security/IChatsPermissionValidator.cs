namespace K1vs.DotChat.Security
{
    using K1vs.DotChat.Chats;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public interface IChatsPermissionValidator
    {
        Task ValidateGetSummary(Guid currentUserId, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateGetPage(Guid currentUserId, IPagedResult<IPersonalizedChat> chatsPage, IChatFilter filter, IPagingOptions pagingOptions, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateGetPage(Guid currentUserId, IPagedResult<IPersonalizedChat> chatsPage, IPagingOptions pagingOptions, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateGet(Guid currentUserId, IPersonalizedChat chat, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateAdd(Guid currentUserId, Guid chatId, IChatInfo chatInfo, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateEditInfo(Guid currentUserId, Guid chatId, IChatInfo chatInfo, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateRemove(Guid currentUserId, Guid chatId, string serviceName, [CallerMemberName] string methodName = null);
    }
}
