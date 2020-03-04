namespace K1vs.DotChat.Commands.Participants
{
    using Common;
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IApplyToChatCommand: ICommand, IChatRelated, ICustomizable
    {
        ChatParticipantType ChatParticipantType { get; }
    }
}
