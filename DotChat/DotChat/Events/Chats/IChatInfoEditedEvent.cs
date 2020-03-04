namespace K1vs.DotChat.Events.Chats
{
    using DotChat.Chats;

    public interface IChatInfoEditedEvent<out TChatInfo> : IChatEvent, IChatRelated, IHasChatInfo<TChatInfo>
        where TChatInfo : IChatInfo
    {
    }
}
