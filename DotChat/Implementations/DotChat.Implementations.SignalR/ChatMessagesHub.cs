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

    public class ChatMessagesHub<TChatMessagesClient, TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo,
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
        private readonly IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage,
            TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment,
            TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter,
            TChatMessagesPagedResult, TPagingOptions> _chatMessagesService;

        public ChatMessagesHub(IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo,
                TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
                TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions> chatMessagesService)
        {
            _chatMessagesService = chatMessagesService;
        }

        public async Task<TChatMessagesPagedResult> GetPage(Guid chatId, IReadOnlyCollection<TMessageFilter> filters,
            TPagingOptions pagingOptions = default)
        {
            return await _chatMessagesService.GetPage(CurrentUserId, chatId, filters, pagingOptions);
        }

        public async Task<TChatMessagesPagedResult> GetPage(Guid chatId, TPagingOptions pagingOptions = default)
        {
            return await _chatMessagesService.GetPage(CurrentUserId, chatId, pagingOptions);
        }

        public async Task Read(Guid chatId, long index)
        {
            await _chatMessagesService.Read(CurrentUserId, chatId, index);
        }

        public async Task<Guid> Add(Guid chatId, Guid? messageId, TChatMessageInfo messageInfo)
        {
            return await _chatMessagesService.Add(CurrentUserId, chatId, messageId, messageInfo);
        }

        public async Task<Guid> Edit(Guid chatId, Guid messageId, TChatMessageInfo messageInfo, Guid? archivedMessageId)
        {
            return await _chatMessagesService.Edit(CurrentUserId, chatId, messageId, messageInfo, archivedMessageId);
        }

        public async Task Remove(Guid chatId, Guid messageId)
        {
            await _chatMessagesService.Remove(CurrentUserId, chatId, messageId);
        }

        protected virtual Guid CurrentUserId => Guid.Parse(Context.User.Identity.Name);
    }
}
