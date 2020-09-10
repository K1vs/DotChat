namespace K1vs.DotChat.Notifiers
{
    using System.Collections.Generic;
    using Events.Participants;
    using Handlers;
    using Participants;

    public interface IChatParticipantNotifier:
        IHandleEvent<IChatParticipantAddedEvent>,
        IHandleEvent<IChatParticipantInvitedEvent>,
        IHandleEvent<IChatParticipantAppliedEvent>,
        IHandleEvent<IChatParticipantRemovedEvent>,
        IHandleEvent<IChatParticipantBlockedEvent>,
        IHandleEvent<IChatParticipantsAppendedEvent>,
        IHandleEvent<IChatParticipantTypeChangedEvent> 
    {
    }
}
