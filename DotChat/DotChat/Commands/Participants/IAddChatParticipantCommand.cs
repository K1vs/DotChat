namespace K1vs.DotChat.Commands.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IAddChatParticipantCommand: ICommand, IChatRelated, IParticipationCandidate
    {
    }
}
