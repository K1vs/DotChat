namespace K1vs.DotChat.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using Events.Participants;
    using Participants;

    public interface IChatParticipantsEventBuilder
    {
        IParticipationResult BuildParticipationResult(IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus);
        IChatParticipantAddedEvent BuildChatParticipantAddedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus);
        IChatParticipantAppliedEvent BuildChatParticipantAppliedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus);
        IChatParticipantInvitedEvent BuildChatParticipantInvitedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus);
        IChatParticipantRemovedEvent BuildChatParticipantRemovedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus);
        IChatParticipantBlockedEvent BuildChatParticipantBlockedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus);
        IChatParticipantTypeChangedEvent BuildChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant);
        IChatParticipantsAppendedEvent BuildChatParticipantsAppendedEvent(Guid initiatorUserId, Guid chatId, IReadOnlyCollection<IParticipationResult> added, IReadOnlyCollection<IParticipationResult> invited);
    }
}
