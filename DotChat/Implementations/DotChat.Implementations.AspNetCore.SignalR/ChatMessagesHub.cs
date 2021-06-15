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

    public abstract class ChatMessagesHub<TChatMessagesClient, TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo,
            TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment,
            TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter,
            TChatMessagesPagedResult, TPagingOptions>
        : Hub<TChatMessagesClient>
        where TChatMessagesClient: class, IChatMessagesClient<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage,
        TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TChatUser : IChatUser
        where TChatMessageCollection : IReadOnlyCollection<TChatMessage>
        where TChatMessage : IChatMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
            TContactMessageCollection, TContactMessage>
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
            TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
            TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
        where TMessageFilter : IMessageFilter
        where TChatMessagesPagedResult : IPagedResult<TChatMessageCollection, TChatMessage>
        where TPagingOptions : IPagingOptions
    {
        protected readonly IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage,
            TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment,
            TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter,
            TChatMessagesPagedResult, TPagingOptions> ChatMessagesService;

        public ChatMessagesHub(IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo,
                TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
                TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions> chatMessagesService)
        {
            ChatMessagesService = chatMessagesService;
        }

        public virtual async Task<TChatMessagesPagedResult> GetPage(Guid chatId, IReadOnlyCollection<TMessageFilter> filters, TPagingOptions pagingOptions)
        {
            return await ChatMessagesService.GetPage(CurrentUserId, chatId, filters, pagingOptions);
        }

        public virtual async Task<TChatMessagesPagedResult> GetPage(Guid chatId, TPagingOptions pagingOptions)
        {
            return await ChatMessagesService.GetPage(CurrentUserId, chatId, pagingOptions);
        }

        public virtual async Task<TChatMessagesPagedResult> GetPage(Guid chatId)
        {
            return await ChatMessagesService.GetPage(CurrentUserId, chatId);
        }

        public virtual async Task Read(Guid chatId, long index, bool force)
        {
            await ChatMessagesService.Read(CurrentUserId, chatId, index, force);
        }

        public virtual async Task<Guid> Add(Guid chatId, Guid? messageId, TChatMessageInfo messageInfo)
        {
            return await ChatMessagesService.Add(CurrentUserId, chatId, messageId, messageInfo);
        }

        public virtual async Task<Guid> Edit(Guid chatId, Guid messageId, TChatMessageInfo messageInfo, Guid? archivedMessageId)
        {
            return await ChatMessagesService.Edit(CurrentUserId, chatId, messageId, messageInfo, archivedMessageId);
        }

        public virtual async Task Remove(Guid chatId, Guid messageId)
        {
            await ChatMessagesService.Remove(CurrentUserId, chatId, messageId);
        }

        protected virtual Guid CurrentUserId => Guid.Parse(Context.User.Identity.Name);
    }
}
