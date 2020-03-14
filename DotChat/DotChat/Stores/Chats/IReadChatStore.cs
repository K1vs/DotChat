namespace K1vs.DotChat.Stores.Chats
{
    using K1vs.DotChat.Chats;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Participants;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Messages;

    public interface IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, in TChatUser, in TChatMessageInfo, in TTextMessage, in TQuoteMessage, in TMessageAttachmentCollection, in TMessageAttachment, in TChatRefMessageCollection, in TChatRefMessage, in TContactMessageCollection, in TContactMessage, in TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, in TPagingOptions>
        where TPersonalizedChatsSummary: IPersonalizedChatsSummary
        where TPersonalizedChatCollection: IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
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
        Task<TPersonalizedChatsSummary> RetrievePersonalizedSummary(Guid userId);
        Task<IReadOnlyCollection<Guid>> RetrieveAllIds(Guid userId);
        Task<TPagedResult> RetrievePersonalizedPage(Guid userId, TChatFilter filter, TPagingOptions pagingOptions);
        Task<TPagedResult> RetrievePersonalizedPage(Guid userId, TPagingOptions pagingOptions);
        Task<TPersonalizedChat> RetrievePersonalized(Guid chatId, Guid userId);
        Task<TChat> Retrieve(Guid chatId);
    }
}
