namespace K1vs.DotChat.Messages.Typed
{
    using Common;
    using K1vs.DotChat.Participants;

    public interface IContactMessage: ICustomizable
    {
        IChatUser Contact { get; }
    }
}
