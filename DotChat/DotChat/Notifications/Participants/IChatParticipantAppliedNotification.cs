namespace K1vs.DotChat.Notifications.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public interface IChatParticipantAppliedNotification<out TChatParticipant> : INotificationBase, IChatRelated, IHasParticipant<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
