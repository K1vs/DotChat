namespace K1vs.DotChat.SystemMessages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Events.Chats;
    using Events.Participants;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface ISystemMessagesBuilder
    {
        IReadOnlyCollection<IChatMessageInfo> BuildChatAddedMessage(IChatAddedEvent @event);
        IReadOnlyCollection<IChatMessageInfo> BuildChatInfoEditedMessages(IChatInfoEditedEvent @event);
        IReadOnlyCollection<IChatMessageInfo> BuildBulkParticipantsAppendedMessages(IChatParticipantBulkAddedInvitedEvent @event);
        IReadOnlyCollection<IChatMessageInfo> BuildChatParticipantAddedMessages(IChatParticipantAddedEvent @event);
        IReadOnlyCollection<IChatMessageInfo> BuildChatParticipantAppliedMessages(IChatParticipantAppliedEvent @event);
        IReadOnlyCollection<IChatMessageInfo> BuildChatParticipantBlockedMessages(IChatParticipantBlockedEvent @event);
        IReadOnlyCollection<IChatMessageInfo> BuildChatParticipantInvitedMessages(IChatParticipantInvitedEvent @event);
        IReadOnlyCollection<IChatMessageInfo> BuildChatParticipantRemovedMessages(IChatParticipantRemovedEvent @event);
        IReadOnlyCollection<IChatMessageInfo> BuildParticipantTypeChangedMessages(IChatParticipantTypeChangedEvent @event);
    }
}
