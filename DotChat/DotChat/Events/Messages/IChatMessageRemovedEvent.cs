namespace K1vs.DotChat.Events.Messages
{
    using Chats;
    using DotChat.Chats;
    using DotChat.Messages;
    using K1vs.DotChat.Common;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;
    using System.Collections.Generic;

    public interface IChatMessageRemovedEvent: IChatMessageEvent, IChatRelated, IHasChatMessage
    {
    }
}
