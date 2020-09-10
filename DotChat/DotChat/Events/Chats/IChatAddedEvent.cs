namespace K1vs.DotChat.Events.Chats
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public interface IChatAddedEvent: IChatEvent, IHasChat
    {
    }
}
