namespace K1vs.DotChat.Events.Messages
{
    using Chats;
    using DotChat.Chats;
    using DotChat.Messages;
    using K1vs.DotChat.Common;

    public interface IChatMessageRemovedEvent: IChatMessageEvent, IChatRelated, IChatMessageRelated, IVersioned
    {
    }
}
