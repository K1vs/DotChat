namespace K1vs.DotChat.Security
{
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Users;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserPermissionValidator
    {
        Task ValidateGet(Guid id, IChatUser chatUser);
        Task ValidateGetPage(Guid currentUserId, IChatUserUserFilter filter, IPagingOptions pagingOptions, IPagedResult<IChatUser> chatUserPagedResult);
        Task ValidateGetPage(Guid currentUserId, IPagingOptions pagingOptions, IPagedResult<IChatUser> chatUserPagedResult);
    }
}
