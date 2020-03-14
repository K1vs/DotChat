namespace K1vs.DotChat.Notifications.Messages
{
    using DotChat.Chats;
    using DotChat.Messages;
    using Events;

    public interface IChatMessagesReadNotification: IChatMessagesNotification, IChatRelated, IIndexed
    {
        bool Force { get; }
    }
}
