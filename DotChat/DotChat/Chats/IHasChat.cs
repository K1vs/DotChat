namespace K1vs.DotChat.Chats
{
    using System.Collections.Generic;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IHasChat
    {
        IChat Chat { get; }
    }
}
