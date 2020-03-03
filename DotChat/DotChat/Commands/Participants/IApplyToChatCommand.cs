namespace K1vs.DotChat.Commands.Participants
{
    using Common;
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IApplyToChatCommand: ICommandBase, IChatRelated, ICustomizable
    {
        ChatParticipantType ChatParticipantType { get; }
    }
}
