namespace K1vs.DotChat.Security
{
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.PersonalizedChats;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPersonalizedChatsPermissionValidator
    {
        Task ValidateGetSummary(Guid currentUserId, IPersonalizedChatsSummary personalizedChatsSummary);
        Task ValidateGetPage(Guid currentUserId, IChatFilter filter, IPagingOptions pagingOptions, IPagedResult<IPersonalizedChat> personalizedChatPagedResult);
        Task ValidateGetPage(Guid currentUserId, IPagingOptions pagingOptions, IPagedResult<IPersonalizedChat> personalizedChatPagedResult);
        Task ValidateGet(Guid currentUserId, Guid chatId, IPersonalizedChat personalizedChat);
    }
}
