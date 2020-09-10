namespace K1vs.DotChat.Notifications.Chats
{
    using DotChat.Chats;
    using Events;

    public interface IChatInfoEditedNotification: IChatsNotification, IChatRelated, IHasChatInfo
    {
    }
}
