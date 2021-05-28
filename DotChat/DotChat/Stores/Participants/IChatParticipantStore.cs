namespace K1vs.DotChat.Stores.Participants
{
    using K1vs.DotChat.Participants;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChatParticipantStore: IReadChatParticipantStore
    {
        Task<IParticipationModificationResult> CreateOrUpdate(Guid chatId, Guid userId, ChatParticipantType participantType, ChatParticipantStatus participantStatus, Guid setterId);
        Task<IReadOnlyCollection<IParticipationModificationResult>> CreateOrUpdateList(Guid chatId, IEnumerable<Guid> chatUserIds, ChatParticipantType participantType, ChatParticipantStatus participantStatus, Guid setterId);
        Task<IParticipationStatusModificationResult> UpdateParticipantStatus(Guid chatId, Guid userId, ChatParticipantStatus participantStatus, Guid setterId);
        Task<IParticipationTypeModificationResult> UpdateParticipantType(Guid chatId, Guid userId, ChatParticipantType participantType, Guid setterId);
        Task UpdateReadIndex(Guid chatId, Guid userId, long index, bool force);
    }
}
