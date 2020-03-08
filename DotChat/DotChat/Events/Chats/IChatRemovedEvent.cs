namespace K1vs.DotChat.Events.Chats
{
    using DotChat.Chats;
    using K1vs.DotChat.Common;

    public interface IChatRemovedEvent<out TChatInfo> : IChatEvent, IChatRelated, IHasChatInfo<TChatInfo>, IVersioned
        where TChatInfo : IChatInfo
    {
    }
}
