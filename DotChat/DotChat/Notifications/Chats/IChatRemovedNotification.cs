namespace K1vs.DotChat.Notifications.Chats
{
    using DotChat.Chats;
    using Events;

    public interface IChatRemovedNotification<out TChatInfo> : INotificationBase, IChatRelated, IHasChatInfo<TChatInfo>
        where TChatInfo : IChatInfo
    {
    }
}
