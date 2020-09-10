namespace K1vs.DotChat.Events.Chats
{
    using DotChat.Chats;
    using K1vs.DotChat.Common;

    public interface IChatInfoEditedEvent: IChatEvent, IChatRelated, IHasChatInfo
    {
    }
}
