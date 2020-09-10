namespace K1vs.DotChat.Stores.Chats
{
    using K1vs.DotChat.Chats;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public interface IChatStore: IReadChatStore
    {
        Task<IChat> Create(Guid chatId, IChatInfo chatInfo, IReadOnlyCollection<IParticipationCandidate> toAdd, IReadOnlyCollection<IParticipationCandidate> toInvite, Guid creatorId);
        Task<IChatInfo> UpdateInfo(Guid chatId, IChatInfo chatInfo, Guid modifierId);
        Task<IChatInfo> Delete(Guid chatId, Guid removerId);
        Task SetTop(Guid chatId, IChatMessage topChatMessage);
    }
}
