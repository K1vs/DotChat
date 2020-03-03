namespace K1vs.DotChat.Stores.Messages
{
    using K1vs.DotChat.Messages;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Chats;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public interface IChatMessageStore<in TChatInfo, in TChatUser, TChatMessageCollection, TChatMessage, in TChatMessageInfo, in TTextMessage, in TQuoteMessage, in TMessageAttachmentCollection, in TMessageAttachment, in TChatRefMessageCollection, in TChatRefMessage, in TContactMessageCollection, in TContactMessage, in TMessageFilter, TPagedResult, in TPagingOptions> : 
        IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions>
        where TChatInfo : IChatInfo
        where TChatUser : IChatUser
        where TChatMessageCollection : IReadOnlyCollection<TChatMessage>
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
        Task Read(Guid chatId, Guid userId, long index);
        Task<TChatMessage> Create(Guid chatId, Guid userId, Guid messageId, TChatMessageInfo messageInfo, DateTime timestamp, long index, Guid creatorId);
        Task<TChatMessage> Update(Guid chatId, Guid messageId, TChatMessageInfo messageInfo, Guid modifierId);
        Task<TChatMessage> Delete(Guid chatId, Guid messageId, Guid removerId);
        Task Archive(Guid chatId, Guid originalMessageId, Guid achievedMessageId, TChatMessage messageInfo, Guid archiverId);
    }
}
