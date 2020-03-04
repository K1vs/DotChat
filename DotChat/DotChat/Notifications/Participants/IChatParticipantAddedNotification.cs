namespace K1vs.DotChat.Notifications.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public interface IChatParticipantAddedNotification<out TChatParticipant> : IChatParticipantsNotification, IChatRelated, IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
