namespace K1vs.DotChat.Notifications.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public interface IChatParticipantInvitedNotification: IChatParticipantsNotification, IChatRelated, IHasParticipant
    {
    }
}
