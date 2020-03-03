namespace K1vs.DotChat.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using Events.Participants;
    using Participants;

    public interface IChatParticipantsEventBuilder<out TParticipationResultCollection, TParticipationResult, TChatParticipant>
        where TParticipationResultCollection: IReadOnlyCollection<TParticipationResult>
        where TParticipationResult: IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        TParticipationResult BuildParticipationResult(TChatParticipant chatParticipant, ChatParticipantStatus previousChatParticipantStatus);
        IChatParticipantAddedEvent<TChatParticipant> BuildChatParticipantAddedEvent(Guid initiatorUserId, Guid chatId, TChatParticipant chatParticipant, ChatParticipantStatus previousChatParticipantStatus);
        IChatParticipantAppliedEvent<TChatParticipant> BuildChatParticipantAppliedEvent(Guid initiatorUserId, Guid chatId, TChatParticipant chatParticipant, ChatParticipantStatus previousChatParticipantStatus);
        IChatParticipantInvitedEvent<TChatParticipant> BuildChatParticipantInvitedEvent(Guid initiatorUserId, Guid chatId, TChatParticipant chatParticipant, ChatParticipantStatus previousChatParticipantStatus);
        IChatParticipantRemovedEvent<TChatParticipant> BuildChatParticipantRemovedEvent(Guid initiatorUserId, Guid chatId, TChatParticipant chatParticipant, ChatParticipantStatus previousChatParticipantStatus);
        IChatParticipantBlockedEvent<TChatParticipant> BuildChatParticipantBlockedEvent(Guid initiatorUserId, Guid chatId, TChatParticipant chatParticipant, ChatParticipantStatus previousChatParticipantStatus);
        IChatParticipantTypeChangedEvent<TChatParticipant> BuildChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId, TChatParticipant chatParticipant, ChatParticipantStatus previousChatParticipantStatus);
        IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant> BuildChatParticipantsAppendedEvent(Guid initiatorUserId, Guid chatId, IReadOnlyCollection<TParticipationResult> added, IReadOnlyCollection<TParticipationResult> invited);
    }
}
