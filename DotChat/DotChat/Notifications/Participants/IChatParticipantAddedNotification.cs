namespace K1vs.DotChat.Notifications.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public interface IChatParticipantAddedNotification<out TChatParticipant> : INotificationBase, IChatRelated, IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
