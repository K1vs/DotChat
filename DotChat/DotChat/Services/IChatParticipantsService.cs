namespace K1vs.DotChat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using Participants;

    public interface IChatParticipantsService
    {
        Task<IPagedResult<IChatParticipant>> GetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<IChatParticipantFilter> filters = default, IPagingOptions pagingOptions = default);
        Task Add(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType);
        Task Invite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType);
        Task Apply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType);
        Task Remove(Guid currentUserId, Guid chatId, Guid userId);
        Task Block(Guid currentUserId, Guid chatId, Guid userId);
        Task ChangeType(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType);
        Task BulkAddInvite(Guid currentUserId, Guid chatId, IParticipantsAddInviteBulk participationCandidates);
        Task Read(Guid currentUserId, Guid chatId, long index, bool force);
    }
}
