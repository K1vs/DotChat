namespace K1vs.DotChat.Commands.Participants
{
    using Common;
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Users;

    public interface IApplyToChatCommand: ICommand, IChatRelated, IUserRelated, IHasParticipantType
    {
    }
}
