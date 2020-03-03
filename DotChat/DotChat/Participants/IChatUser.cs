namespace K1vs.DotChat.Participants
{
    using System;
    using Common;

    public interface IChatUser: IUserRelated, ICustomizable
    {
        string Name { get; }

        string Details { get; set; }

        Guid? AvatarId { get; }

        bool InviteOnly { get; }

        bool CanCreateChat { get; }
    }
}
