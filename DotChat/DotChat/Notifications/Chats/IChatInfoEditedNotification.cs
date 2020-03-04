namespace K1vs.DotChat.Notifications.Chats
{
    using DotChat.Chats;
    using Events;

    public interface IChatInfoEditedNotification<out TChatInfo> 
        : IChatsNotification, IChatRelated, IHasChatInfo<TChatInfo>
        where TChatInfo : IChatInfo
    {
    }
}
