namespace K1vs.DotChat.Messages.Typed
{
    using Common;
    using K1vs.DotChat.Participants;
    using K1vs.DotChat.Users;

    public interface IContactMessage: ICustomizable
    {
        IChatUser Contact { get; }
    }
}
