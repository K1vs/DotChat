namespace K1vs.DotChat.Chats
{
    using Common;
    using System;

    public interface IChatInfo: ICustomizable, IVersioned
    {
        string Name { get; }
        string Description { get; }
        Guid ImageId { get; }
        ChatPrivacyMode PrivacyMode { get; }
    }
}