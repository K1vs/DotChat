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

    public class ChatParticipantsEventBuilder: IChatParticipantsEventBuilder<List<ParticipationResult>, ParticipationResult, ChatParticipant>
    {
        public ParticipationResult BuildParticipationResult(ChatParticipant chatParticipant,
            ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ParticipationResult(previousChatParticipantStatus, chatParticipant);
        }

        public IChatParticipantAddedEvent<ChatParticipant> BuildChatParticipantAddedEvent(Guid initiatorUserId, Guid chatId, ChatParticipant chatParticipant,
            ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantAddedEvent<ChatParticipant>(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantAppliedEvent<ChatParticipant> BuildChatParticipantAppliedEvent(Guid initiatorUserId, Guid chatId,
            ChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantAppliedEvent<ChatParticipant>(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantInvitedEvent<ChatParticipant> BuildChatParticipantInvitedEvent(Guid initiatorUserId, Guid chatId,
            ChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantInvitedEvent<ChatParticipant>(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantRemovedEvent<ChatParticipant> BuildChatParticipantRemovedEvent(Guid initiatorUserId, Guid chatId,
            ChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantRemovedEvent<ChatParticipant>(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantBlockedEvent<ChatParticipant> BuildChatParticipantBlockedEvent(Guid initiatorUserId, Guid chatId,
            ChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantBlockedEvent(initiatorUserId, chatId, chatParticipant, previousChatParticipantStatus);
        }

        public IChatParticipantTypeChangedEvent<ChatParticipant> BuildChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId,
            ChatParticipant chatParticipant, ChatParticipantStatus? previousChatParticipantStatus)
        {
            return new ChatParticipantTypeChangedEvent(initiatorUserId, chatId, chatParticipant);
        }

        public IChatParticipantsAppendedEvent<List<ParticipationResult>, ParticipationResult, ChatParticipant> BuildChatParticipantsAppendedEvent(Guid initiatorUserId, Guid chatId, IReadOnlyCollection<ParticipationResult> added,
            IReadOnlyCollection<ParticipationResult> invited)
        {
            return new ChatParticipantsAppendedEvent(initiatorUserId, chatId, added.ToList(), invited.ToList());
        }
    }
}
