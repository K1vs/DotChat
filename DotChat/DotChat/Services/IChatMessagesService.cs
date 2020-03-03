namespace K1vs.DotChat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Chats;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessagesService<in TChatInfo, in TChatUser, in TChatMessageCollection, in TChatMessage, in TChatMessageInfo, in TTextMessage, in TQuoteMessage, in TMessageAttachmentCollection, in TMessageAttachment, in TChatRefMessageCollection, in TChatRefMessage, in TContactMessageCollection, in TContactMessage, in TMessageFilter, TPagedResult, in TPagingOptions>
        where TChatInfo : IChatInfo
        where TChatUser : IChatUser
        where TChatMessageCollection: IReadOnlyCollection<TChatMessage>
        where TChatMessage : IChatMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TChatMessageCollection, TChatMessage>
        where TPagingOptions : IPagingOptions
    {
        Task<TPagedResult> GetAll(Guid currentUserId, Guid chatId, IReadOnlyCollection<TMessageFilter> filters = default, TPagingOptions pagingOptions = default);

        Task Read(Guid currentUserId, Guid chatId, long index);
        Task<Guid> Add(Guid currentUserId, Guid chatId, Guid? messageId, TChatMessageInfo messageInfo);
        Task<Guid> Edit(Guid currentUserId, Guid chatId, Guid messageId, TChatMessageInfo messageInfo, Guid? archivedMessageId);
        Task Remove(Guid currentUserId, Guid chatId, Guid messageId);
    }
}
