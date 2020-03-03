namespace K1vs.DotChat.Stores.Users
{
    using K1vs.DotChat.Participants;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReadUserStore<TChatUser>
        where TChatUser : IChatUser
    {
        Task<TChatUser> Retrieve(Guid userId);
        Task<IReadOnlyCollection<TChatUser>> Retrieve(IEnumerable<Guid> userIds);
        TChatUser Customize(TChatUser user, string style, string metadata);
    }
}
