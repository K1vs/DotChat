namespace K1vs.DotChat.Notifications.Messages
{
    using DotChat.Chats;
    using DotChat.Messages;
    using Events;

    public interface IChatMessagesReadNotification: INotificationBase, IChatRelated, IIndexed
    {
    }
}
