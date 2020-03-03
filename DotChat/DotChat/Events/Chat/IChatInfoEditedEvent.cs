namespace K1vs.DotChat.Events.Chat
{
    using Chats;

    public interface IChatInfoEditedEvent<out TChatInfo> : IEventBase, IChatRelated, IHasChatInfo<TChatInfo>
        where TChatInfo : IChatInfo
    {
    }
}
