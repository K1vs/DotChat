namespace K1vs.DotChat.Commands.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IRemoveChatParticipantCommand: ICommand, IChatRelated, IUserRelated
    {
    }
}
