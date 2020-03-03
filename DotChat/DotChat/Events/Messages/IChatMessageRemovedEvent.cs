namespace K1vs.DotChat.Events.Messages
{
    using Chats;
    using DotChat.Messages;

    public interface IChatMessageRemovedEvent: IEventBase, IChatRelated, IChatMessageRelated
    {
    }
}
