namespace K1vs.DotChat.Users
{
    using System;
    using Common;

    public interface IChatUser: IUserRelated, ICustomizable, IVersioned
    {
        string Name { get; }

        string Details { get; set; }

        Guid? AvatarId { get; }

        bool InviteOnly { get; }

        bool CanCreateChat { get; }
    }
}
