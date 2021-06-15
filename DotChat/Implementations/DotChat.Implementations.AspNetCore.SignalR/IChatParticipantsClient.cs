namespace K1vs.DotChat.Implementations.AspNetCore.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Events.Participants;
    using Notifications.Participants;
    using Participants;

    public interface IChatParticipantsClient<in TParticipationResultCollection, in TParticipationResult, in TChatParticipant>
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        Task ChatParticipantAdded(IChatParticipantAddedNotification<TChatParticipant> notification);
        Task ChatParticipantApplied(IChatParticipantAppliedNotification<TChatParticipant> notification);
        Task ChatParticipantInvited(IChatParticipantInvitedNotification<TChatParticipant> notification);
        Task ChatParticipantRemoved(IChatParticipantRemovedNotification<TChatParticipant> notification);
        Task ChatParticipantBlocked(IChatParticipantBlockedNotification<TChatParticipant> notification);
        Task ChatParticipantsAppended(IChatParticipantsAppendedNotification<TParticipationResultCollection, TParticipationResult, TChatParticipant> notification);
        Task ChatParticipantTypeChanged(IChatParticipantTypeChangedNotification<TChatParticipant> notification);
    }
}
