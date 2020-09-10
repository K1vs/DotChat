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
        IChatMessageInfo BuildChatInfoEditedMessage(IChatInfoEditedEvent @event);
        IReadOnlyCollection<IChatMessageInfo> BuildBulkParticipantsAppendedMessages(IChatParticipantsAppendedEvent @event);
        IChatMessageInfo BuildChatParticipantAddedMessage(IChatParticipantAddedEvent @event);
        IChatMessageInfo BuildChatParticipantAppliedMessage(IChatParticipantAppliedEvent @event);
        IChatMessageInfo BuildChatParticipantBlockedMessage(IChatParticipantBlockedEvent @event);     
        IChatMessageInfo BuildChatParticipantInvitedMessage(IChatParticipantInvitedEvent @event);
        IChatMessageInfo BuildChatParticipantRemovedMessage(IChatParticipantRemovedEvent @event);
        IChatMessageInfo BuildParticipantTypeChangedMessage(IChatParticipantTypeChangedEvent @event);
    }
}
