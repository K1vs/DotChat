namespace K1vs.DotChat.Events.Participants
{
    using Chats;
    using DotChat.Chats;
    using K1vs.DotChat.Participants;

    public interface IChatParticipantBlockedEvent<out TChatParticipant> : IChatParticipantEvent, IChatRelated, IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
