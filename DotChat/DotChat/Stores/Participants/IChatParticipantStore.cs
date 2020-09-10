namespace K1vs.DotChat.Stores.Participants
{
    using K1vs.DotChat.Participants;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChatParticipantStore: IReadChatParticipantStore
    {
        Task<IChatParticipant> Set(Guid chatId, IChatUser chatUser, ChatParticipantType participantType, ChatParticipantStatus participantStatus, Guid setterId);
        Task<IChatParticipant> Set(Guid chatId, IChatUser chatUser, ChatParticipantStatus participantStatus, Guid setterId);
        Task<IReadOnlyCollection<IChatParticipant>> Set(Guid chatId, IEnumerable<IChatUser> chatUsers, ChatParticipantType participantType, ChatParticipantStatus participantStatus, Guid setterId);
        Task<IChatParticipant> ChangeType(Guid chatId, Guid userId, ChatParticipantType participantType, Guid setterId);
    }
}
