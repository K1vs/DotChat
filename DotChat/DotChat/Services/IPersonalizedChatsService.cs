using K1vs.DotChat.Chats;
using K1vs.DotChat.Common.Filters;
using K1vs.DotChat.Common.Paging;
using K1vs.DotChat.PersonalizedChats;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace K1vs.DotChat.Services
{
    interface IPersonalizedChatsService
    {
        Task<IPersonalizedChatsSummary> GetSummary(Guid currentUserId);
        Task<IPagedResult<IPersonalizedChat>> GetPage(Guid currentUserId, IChatFilter filter = default, IPagingOptions pagingOptions = default);
        Task<IPersonalizedChat> Get(Guid currentUserId, Guid chatId);
    }
}
