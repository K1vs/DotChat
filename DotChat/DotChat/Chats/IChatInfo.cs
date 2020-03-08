namespace K1vs.DotChat.Chats
{
    using Common;

    public interface IChatInfo: ICustomizable, IVersioned
    {
        string Name { get; }
        string Description { get; }
        ChatPrivacyMode PrivacyMode { get; }
    }
}