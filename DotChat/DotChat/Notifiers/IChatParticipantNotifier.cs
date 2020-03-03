namespace K1vs.DotChat.Notifiers
{
    using System.Collections.Generic;
    using Events.Participants;
    using Handlers;
    using Participants;

    public interface IChatParticipantNotifier<in TParticipationResultCollection, in TParticipationResult, in TChatParticipant> :
        IHandleEvent<IChatParticipantAddedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantInvitedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantAppliedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantRemovedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantBlockedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant>>,
        IHandleEvent<IChatParticipantTypeChangedEvent<TChatParticipant>> 
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
