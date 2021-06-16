namespace K1vs.DotChat.Implementations.AspNetCore.SignalR
{
    using K1vs.DotChat.Chats;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Configuration;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;
    using K1vs.DotChat.Services;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class ChatsHub<TChatsClient, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo,
            TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, 
            TParticipationCandidate, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage,
            TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions>
        : Hub<TChatsClient>
        where TChatsClient: class, IChatsClient<TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TPersonalizedChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection : IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidates : IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatUser : IChatUser
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        protected readonly IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant,
            TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, 
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, 
            TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> ChatsService;

        public ChatsHub(IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant,
            TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage,
            TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> chatsService)
        {
            ChatsService = chatsService;
        }

        public virtual async Task<TPersonalizedChatsSummary> GetSummary()
        {
            return await ChatsService.GetSummary(CurrentUserId);
        }

        public virtual async Task<TPagedResult> GetPage(TChatFilter filter, TPagingOptions pagingOptions)
        {
            if (filter != null && pagingOptions != null)
            {
                return await ChatsService.GetPage(CurrentUserId, filter, pagingOptions);
            }
            else if (pagingOptions != null)
            {
                return await ChatsService.GetPage(CurrentUserId, pagingOptions);
            }
            else
            {
                return await ChatsService.GetPage(CurrentUserId);
            }
        }

        public virtual async Task<TPersonalizedChat> Get(Guid chatId)
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
