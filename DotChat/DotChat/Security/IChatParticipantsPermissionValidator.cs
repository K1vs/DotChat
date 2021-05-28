namespace K1vs.DotChat.Security
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using Participants;

    public interface IChatParticipantsPermissionValidator
    {
        Task GetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<IChatParticipantFilter> filters, IPagingOptions pagingOptions, IPagedResult<IChatParticipant> chatParticipantPagedResult);
        Task GetPage(Guid currentUserId, Guid chatId, IPagingOptions pagingOptions, IPagedResult<IChatParticipant> chatParticipantPagedResult);
        Task ValidateAdd(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType);
        Task ValidateInvite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType);
        Task ValidateApply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType);
        Task ValidateRemove(Guid currentUserId, Guid chatId, Guid userId);
        Task ValidateBlock(Guid currentUserId, Guid chatId, Guid userId);
        Task ValidateChangeType(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType);
        Task ValidateBulkAddInvite(Guid currentUserId, Guid chatId, IParticipantsAddInviteBulk participationCandidates);
        Task ValidateRead(Guid currentUserId, Guid chatId, long index, bool force);
    }
}
