namespace K1vs.DotChat.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Events.Chats;
    using Events.Participants;
    using Handlers;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IChatSystemMessagesWorker:
        IHandleEvent<IChatParticipantAddedEvent>,
        IHandleEvent<IChatParticipantInvitedEvent>,
        IHandleEvent<IChatParticipantAppliedEvent>,
        IHandleEvent<IChatParticipantRemovedEvent>,
        IHandleEvent<IChatParticipantBlockedEvent>,
        IHandleEvent<IChatParticipantsAppendedEvent>,
        IHandleEvent<IChatAddedEvent>,
        IHandleEvent<IChatInfoEditedEvent>
    {
    }
}
