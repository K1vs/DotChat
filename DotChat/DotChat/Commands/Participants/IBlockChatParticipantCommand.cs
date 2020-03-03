namespace K1vs.DotChat.Commands.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IBlockChatParticipantCommand : ICommandBase, IChatRelated, IUserRelated
    {
    }
}
