namespace K1vs.DotChat.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using Events.Participants;
    using Participants;

    public interface IChatParticipantsEventBuilder
    {
        IChatParticipantAddedEvent BuildChatParticipantAddedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationModificationResult hasParticipationModificationResult);
        IChatParticipantAppliedEvent BuildChatParticipantAppliedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationModificationResult hasParticipationModificationResult);
        IChatParticipantInvitedEvent BuildChatParticipantInvitedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationModificationResult hasParticipationModificationResult);
        IChatParticipantRemovedEvent BuildChatParticipantRemovedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationStatusModificationResult participationStatusModificationResult);
        IChatParticipantBlockedEvent BuildChatParticipantBlockedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationStatusModificationResult participationStatusModificationResult);
        IChatParticipantTypeChangedEvent BuildChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationTypeModificationResult participationTypeModificationResult);
        IChatParticipantBulkAddedInvitedEvent BuildChatParticipantsBulkAddedInvitedEvent(Guid initiatorUserId, Guid chatId, 
            IReadOnlyCollection<IParticipationModificationResult> addedParticipationModificationResults, IReadOnlyCollection<IParticipationModificationResult> invitedParticipationModificationResults);
        IChatParticipantReadEvent BuildChatParticipantReadEvent(Guid initiatorUserId, Guid chatId, long index, bool force);
    }
}
