namespace K1vs.DotChat.Implementations.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Events.Participants;
    using Notifications.Participants;
    using Participants;

    public interface IChatParticipantsClient
    {
        Task ChatParticipantAdded(IChatParticipantAddedNotification notification);
        Task ChatParticipantApplied(IChatParticipantAppliedNotification notification);
        Task ChatParticipantInvited(IChatParticipantInvitedNotification notification);
        Task ChatParticipantRemoved(IChatParticipantRemovedNotification notification);
        Task ChatParticipantBlocked(IChatParticipantBlockedNotification notification);
        Task ChatParticipantsAppended(IChatParticipantsAppendedNotification notification);
        Task ChatParticipantTypeChanged(IChatParticipantTypeChangedNotification notification);
    }
}
