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

    public interface IChatsPermissionValidator<in TPersonalizedChatCollection, in TPersonalizedChat, in TChatInfo, in TChatParticipantCollection, in TChatParticipant, in TChatFilter, TChatUserFilter, TMessageFilter, in TPagedResult, in TPagingOptions>
        where TPersonalizedChatCollection: IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TChatFilter: IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        Task ValidateGetSummary(Guid currentUserId, string serviceName, [CallerMemberName] string methodName = null);

        Task ValidateGetPage(Guid currentUserId, TPagedResult chatsPage, TChatFilter filter, TPagingOptions pagingOptions, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateGetPage(Guid currentUserId, TPagedResult chatsPage, TPagingOptions pagingOptions, string serviceName, [CallerMemberName] string methodName = null);

        Task ValidateGet(Guid currentUserId, TPersonalizedChat chat, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateAdd(Guid currentUserId, Guid chatId, TChatInfo chatInfo, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateEditInfo(Guid currentUserId, Guid chatId, TChatInfo chatInfo, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateRemove(Guid currentUserId, Guid chatId, string serviceName, [CallerMemberName] string methodName = null);
    }
}
