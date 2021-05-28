namespace K1vs.DotChat.PersonalizedChats
{
    using System.Collections.Generic;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IHasPersonalizedChat
    {
        IPersonalizedChat PersonalizedChat { get; }
    }
}
