namespace K1vs.DotChat.Commands.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Users;

    public interface IAddChatParticipantCommand: ICommand, IChatRelated, IUserRelated, IHasParticipantType
    {
    }
}
