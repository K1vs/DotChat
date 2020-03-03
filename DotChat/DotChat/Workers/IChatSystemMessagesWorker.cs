namespace K1vs.DotChat.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Events.Chat;
    using Events.Participants;
    using Handlers;
    using Participants;

    public interface IChatSystemMessagesWorker<in TChat, in TChatInfo, in TParticipationResultCollection, in TParticipationResult, in TChatParticipantCollection, in TChatParticipant> :
        IHandleEvent<IChatParticipantAddedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantInvitedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantAppliedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantRemovedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantBlockedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant>>,
        IHandleEvent<IChatAddedEvent<TChat, TChatParticipantCollection, TChatParticipant>>,
        IHandleEvent<IChatInfoEditedEvent<TChatInfo>>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo : IChatInfo
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
