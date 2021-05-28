namespace K1vs.DotChat.Services
{
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Users;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<IChatUser> Get(Guid id);
        Task<IPagedResult<IChatUser>> GetPage(Guid currentUserId, IChatUserUserFilter filter = default, IPagingOptions pagingOptions = default);
    }
}
