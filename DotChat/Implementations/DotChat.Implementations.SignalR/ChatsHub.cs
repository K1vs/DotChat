namespace K1vs.DotChat.Implementations.SignalR
{
    using K1vs.DotChat.Chats;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Configuration;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;
    using K1vs.DotChat.Services;
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class ChatsHub<TChatsClient, TChatFilter, TPagingOptions, TChatInfo, TParticipationCandidates>
        : Hub<TChatsClient>
        where TChatsClient: class, IChatsClient
        where TChatFilter : IChatFilter
        where TPagingOptions : IPagingOptions
        where TChatInfo: IChatInfo
        where TParticipationCandidates: IParticipantsAddInviteBulk
    {
        protected readonly IChatsService ChatsService;

        public ChatsHub(IChatsService chatsService)
        {
            ChatsService = chatsService;
        }

        public virtual async Task<IPersonalizedChatsSummary> GetSummary()
        {
            return await ChatsService.GetSummary(CurrentUserId);
        }

        public virtual async Task<IPagedResult<IPersonalizedChat>> GetPage(TChatFilter filter, TPagingOptions pagingOptions)
        {
            return await ChatsService.GetPage(CurrentUserId, filter, pagingOptions);
        }

        public virtual async Task<IPagedResult<IPersonalizedChat>> GetPage(TPagingOptions pagingOptions)
        {
            return await ChatsService.GetPage(CurrentUserId, pagingOptions);
        }

        public virtual async Task<IPagedResult<IPersonalizedChat>> GetPage()
        {
            return await ChatsService.GetPage(CurrentUserId);
        }

        public virtual async Task<IPersonalizedChat> Get(Guid chatId)
        {
            return await ChatsService.Get(CurrentUserId, chatId);
        }

        public virtual async Task<Guid> Add(Guid? chatId, TChatInfo chatInfo, TParticipationCandidates participationCandidates)
        {
            return await ChatsService.Add(CurrentUserId, chatId, chatInfo, participationCandidates);
        }

        public virtual async Task EditInfo(Guid chatId, TChatInfo chatInfo)
        {
            await ChatsService.EditInfo(CurrentUserId, chatId, chatInfo);
        }

        public virtual async Task Remove(Guid chatId)
        {
            await ChatsService.Remove(CurrentUserId, chatId);
        }

        protected virtual Guid CurrentUserId => Guid.Parse(Context.User.Identity.Name);
    }
}
