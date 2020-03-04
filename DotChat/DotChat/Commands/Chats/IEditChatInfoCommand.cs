namespace K1vs.DotChat.Commands.Chats
{
    using DotChat.Chats;

    public interface IEditChatInfoCommand<out TChatInfo> : ICommand, IChatRelated, IHasChatInfo<TChatInfo>
        where TChatInfo : IChatInfo
    {
    }
}
