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
    using Events.Participants;
    using Models.Participants;
    using Participants;

    public class ChatParticipantsEventBuilder: IChatParticipantsEventBuilder
    {
        public IParticipationResult BuildParticipationResult(IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ParticipationResult(previousChatParticipantStatus, chatParticipant);
        }

        public IChatParticipantAddedEvent BuildChatParticipantAddedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantAddedEvent(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantAppliedEvent BuildChatParticipantAppliedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantAppliedEvent(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantInvitedEvent BuildChatParticipantInvitedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantInvitedEvent(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantRemovedEvent BuildChatParticipantRemovedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantRemovedEvent(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantBlockedEvent BuildChatParticipantBlockedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantBlockedEvent(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantTypeChangedEvent BuildChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant chatParticipant)
        {
            return new ChatParticipantTypeChangedEvent(initiatorUserId, chatId, chatParticipant);
        }

        public IChatParticipantsAppendedEvent BuildChatParticipantsAppendedEvent(Guid initiatorUserId, Guid chatId, IReadOnlyCollection<IParticipationResult> added, IReadOnlyCollection<IParticipationResult> invited)
        {
            return new ChatParticipantsAppendedEvent(initiatorUserId, chatId, added.ToList(), invited.ToList());
        }
    }
}
