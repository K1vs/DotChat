namespace K1vs.DotChat.Events.Messages
{
    using System.Collections.Generic;
    using Chats;
    using DotChat.Chats;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;

    public interface IChatMessageEditedEvent : IChatMessageEvent, IChatRelated, IHasChatMessage
    {
    }
}
