namespace K1vs.DotChat.Events.Participants
{
    using Chats;
    using K1vs.DotChat.Participants;

    public interface IChatParticipantBlockedEvent<out TChatParticipant> : IEventBase, IChatRelated, IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
