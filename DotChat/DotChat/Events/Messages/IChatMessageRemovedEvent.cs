namespace K1vs.DotChat.Events.Messages
{
    using Chats;
    using DotChat.Chats;
    using DotChat.Messages;

    public interface IChatMessageRemovedEvent: IChatMessageEvent, IChatRelated, IChatMessageRelated
    {
    }
}
