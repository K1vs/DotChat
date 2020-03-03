namespace K1vs.DotChat.Events.Chat
{
    using K1vs.DotChat.Chats;

    public interface IChatRemovedEvent<out TChatInfo> : IEventBase, IChatRelated, IHasChatInfo<TChatInfo>
        where TChatInfo : IChatInfo
    {
    }
}
