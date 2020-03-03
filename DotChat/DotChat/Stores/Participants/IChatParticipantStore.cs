namespace K1vs.DotChat.Stores.Participants
{
    using K1vs.DotChat.Participants;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChatParticipantStore<TChatParticipant, in TChatUser> : IReadChatParticipantStore<TChatParticipant>
        where TChatUser : IChatUser
        where TChatParticipant : IChatParticipant
    {
        Task<TChatParticipant> Set(Guid chatId, TChatUser chatUser, ChatParticipantType participantType, ChatParticipantStatus participantStatus, Guid setterId);
        Task<TChatParticipant> Set(Guid chatId, TChatUser chatUser, ChatParticipantStatus participantStatus, Guid setterId);
        Task<IReadOnlyCollection<TChatParticipant>> Set(Guid chatId, IEnumerable<TChatUser> chatUsers, ChatParticipantType participantType, ChatParticipantStatus participantStatus, Guid setterId);
        Task<TChatParticipant> ChangeType(Guid chatId, Guid userId, ChatParticipantType participantType, Guid setterId);
    }
}
