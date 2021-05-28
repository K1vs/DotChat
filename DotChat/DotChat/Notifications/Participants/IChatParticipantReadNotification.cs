namespace K1vs.DotChat.Notifications.Participants
{
    using DotChat.Chats;
    using DotChat.Messages;
    using Events;
    using K1vs.DotChat.Notifications.Messages;

    public interface IChatParticipantReadNotification: IChatParticipantsNotification, IChatRelated, IIndexed
    {
        bool Force { get; }
    }
}
