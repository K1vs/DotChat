namespace K1vs.DotChat.Chats
{
    public interface IHasChatInfo<out TChatInfo>
        where TChatInfo : IChatInfo
    {
        TChatInfo ChatInfo { get; }
    }
}
