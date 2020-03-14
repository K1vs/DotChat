namespace K1vs.DotChat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Chats;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, in TChatInfo, TChatParticipantCollection, TChatParticipant, in TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, in TChatUser, in TChatMessageInfo, in TTextMessage, in TQuoteMessage, in TMessageAttachmentCollection, in TMessageAttachment, in TChatRefMessageCollection, in TChatRefMessage, in TContactMessageCollection, in TContactMessage, in TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, in TPagingOptions>
        where TPersonalizedChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection: IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat: IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo: IChatInfo
        where TChatParticipantCollection: IReadOnlyCollection<TChatParticipant>
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
        where TPagedResult: IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions: IPagingOptions
    {
        Task<TPersonalizedChatsSummary> GetSummary(Guid currentUserId);
        Task<TPagedResult> GetPage(Guid currentUserId, TChatFilter filter, TPagingOptions pagingOptions = default);
        Task<TPagedResult> GetPage(Guid currentUserId, TPagingOptions pagingOptions = default);
        Task<TPersonalizedChat> Get(Guid currentUserId, Guid chatId);
        Task<Guid> Add(Guid currentUserId, TChatInfo chatInfo, TParticipationCandidates participationCandidates);
        Task EditInfo(Guid currentUserId, Guid chatId, TChatInfo chatInfo);
        Task Remove(Guid currentUserId, Guid chatId);
    }
}
