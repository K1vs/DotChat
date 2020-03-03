namespace K1vs.DotChat.Security
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Chats;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessagesPermissionValidator<out TChatInfo, out TChatUser, in TChatMessageCollection, in TChatMessage, in TChatMessageInfo, in TTextMessage, in TQuoteMessage, 
        out TMessageAttachmentCollection, out TMessageAttachment, out TChatRefMessageCollection, out TChatRefMessage, out TContactMessageCollection, out TContactMessage, in TMessageFilter, in TPagedResult, in TPagingOptions>
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
        Task ValidateGetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<TMessageFilter> filters, TPagingOptions pagingOptions, TPagedResult messagesPage, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateRead(Guid currentUserId, Guid chatId, long index, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateAdd(Guid currentUserId, Guid chatId, TChatMessageInfo messageInfo, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateRemove(Guid currentUserId, Guid chatId, Guid messageId, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateEdit(Guid currentUserId, Guid chatId, Guid messageId, TChatMessageInfo newMessage, string serviceName, [CallerMemberName] string methodName = null);
    }
}
