namespace K1vs.DotChat.Notifications.Participants
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public interface IChatParticipantBulkAddedInvitedNotification: IChatParticipantsNotification, IChatRelated
    {
        IReadOnlyCollection<IParticipationModificationResult> Added { get; }
        IReadOnlyCollection<IParticipationModificationResult> Invited { get; }
    }
}
