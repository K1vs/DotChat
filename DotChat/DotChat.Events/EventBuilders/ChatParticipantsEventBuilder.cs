namespace K1vs.DotChat.Basic.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.EventBuilders;
    using DotChat.Events.Participants;
    using DotChat.Participants;
    using K1vs.DotChat.Events.Messages;

    public class ChatParticipantsEventBuilder: IChatParticipantsEventBuilder
    {
        public IChatParticipantAddedEvent BuildChatParticipantAddedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationModificationResult hasParticipationModificationResult)
        {
            return new ChatParticipantAddedEvent(initiatorUserId, chatId, chatParticipant, hasParticipationModificationResult);
        }

        public IChatParticipantAppliedEvent BuildChatParticipantAppliedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationModificationResult hasParticipationModificationResult)
        {
            return new ChatParticipantAppliedEvent(initiatorUserId, chatId, chatParticipant, hasParticipationModificationResult);
        }

        public IChatParticipantInvitedEvent BuildChatParticipantInvitedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationModificationResult hasParticipationModificationResult)
        {
            return new ChatParticipantInvitedEvent(initiatorUserId, chatId, chatParticipant, hasParticipationModificationResult);
        }

        public IChatParticipantRemovedEvent BuildChatParticipantRemovedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationStatusModificationResult participationStatusModificationResult)
        {
            return new ChatParticipantRemovedEvent(initiatorUserId, chatId, chatParticipant, participationStatusModificationResult);
        }

        public IChatParticipantBlockedEvent BuildChatParticipantBlockedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationStatusModificationResult participationStatusModificationResult)
        {
            return new ChatParticipantBlockedEvent(initiatorUserId, chatId, chatParticipant, participationStatusModificationResult);
        }

        public IChatParticipantTypeChangedEvent BuildChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, IParticipationTypeModificationResult participationTypeModificationResult)
        {
            return new ChatParticipantTypeChangedEvent(initiatorUserId, chatId, chatParticipant, participationTypeModificationResult);
        }

        public IChatParticipantBulkAddedInvitedEvent BuildChatParticipantsBulkAddedInvitedEvent(Guid initiatorUserId, Guid chatId, IReadOnlyCollection<IParticipationModificationResult> addedParticipationModificationResults, IReadOnlyCollection<IParticipationModificationResult> invitedParticipationModificationResults)
        {
            return new ChatParticipantsAppendedEvent(initiatorUserId, chatId, addedParticipationModificationResults, invitedParticipationModificationResults);
        }

        public IChatParticipantReadEvent BuildChatParticipantReadEvent(Guid initiatorUserId, Guid chatId, long index, bool force)
        {
            return new ChatParticipantReadEvent(initiatorUserId, chatId, index, force);
        }
    }
}
