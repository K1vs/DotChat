namespace K1vs.DotChat.Commands.Chats
{
    using DotChat.Chats;

    public interface IEditChatInfoCommand: ICommand, IChatRelated, IHasChatInfo
    {
    }
}
