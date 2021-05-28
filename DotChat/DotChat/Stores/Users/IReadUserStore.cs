namespace K1vs.DotChat.Stores.Users
{
    using K1vs.DotChat.Participants;
    using K1vs.DotChat.Users;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReadUserStore
    {
        Task<IChatUser> Retrieve(Guid userId);
        Task<IReadOnlyCollection<IChatUser>> Retrieve(IEnumerable<Guid> userIds);
    }
}
