namespace K1vs.DotChat.Commands.Chats
{
    using DotChat.Chats;

    public interface IEditChatInfoCommand<out TChatInfo> : ICommandBase, IChatRelated, IHasChatInfo<TChatInfo>
        where TChatInfo : IChatInfo
    {
    }
}
