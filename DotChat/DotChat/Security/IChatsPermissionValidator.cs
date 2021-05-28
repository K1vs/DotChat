namespace K1vs.DotChat.Security
{
    using K1vs.DotChat.Chats;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public interface IChatsPermissionValidator
    {
        Task ValidateGetSummary(Guid currentUserId, IChatsSummary chatsSummary);
        Task ValidateGetPage(Guid currentUserId, IChatFilter filter, IPagingOptions pagingOptions, IPagedResult<IChat> chatPage);
        Task ValidateGetPage(Guid currentUserId, IPagingOptions pagingOptions, IPagedResult<IChat> chatPage);
        Task ValidateGet(Guid currentUserId, IChat chat);
        Task ValidateAdd(Guid currentUserId, Guid chatId, IChatInfo chatInfo, IParticipantsAddInviteBulk participationCandidates);
        Task ValidateEditInfo(Guid currentUserId, Guid chatId, IChatInfo chatInfo);
        Task ValidateRemove(Guid currentUserId, Guid chatId);
    }
}
