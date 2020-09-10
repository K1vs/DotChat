namespace K1vs.DotChat.Notifications.Participants
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public interface IChatParticipantsAppendedNotification: IChatParticipantsNotification, IChatRelated
    {
        IReadOnlyCollection<IParticipationResult> Added { get; }
        IReadOnlyCollection<IParticipationResult> Invited { get; }
    }
}
