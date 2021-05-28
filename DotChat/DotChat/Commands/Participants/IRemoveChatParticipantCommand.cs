namespace K1vs.DotChat.Commands.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Users;

    public interface IRemoveChatParticipantCommand: ICommand, IChatRelated, IUserRelated
    {
    }
}
