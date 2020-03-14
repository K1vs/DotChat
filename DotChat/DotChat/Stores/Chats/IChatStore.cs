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

    public interface IChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, in TParticipationCandidateCollection, TParticipationCandidate, in TChatUser, in TChatMessageInfo, in TTextMessage, in TQuoteMessage, in TMessageAttachmentCollection, in TMessageAttachment, in TChatRefMessageCollection, in TChatRefMessage, in TContactMessageCollection, in TContactMessage, in TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, in TPagingOptions> : 
        IReadChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions>
        where TChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection : IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo: IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
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
        Task<TChat> Create(Guid chatId, TChatInfo chatInfo, TParticipationCandidateCollection toAdd, TParticipationCandidateCollection toInvite, Guid creatorId);
        Task<TChatInfo> UpdateInfo(Guid chatId, TChatInfo chatInfo, Guid modifierId);
        Task<TChatInfo> Delete(Guid chatId, Guid removerId);
        Task SetTop(Guid chatId, DateTime lastTimestamp, long topIndex);
    }
}
