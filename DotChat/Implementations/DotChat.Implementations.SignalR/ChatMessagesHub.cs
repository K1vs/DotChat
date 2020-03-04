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

    public class ChatMessagesHub<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo,
            TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment,
            TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter,
            TChatMessagesPagedResult, TPagingOptions>
        : Hub, IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo,
            TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment,
            TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter,
            TChatMessagesPagedResult, TPagingOptions>
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

        public async Task<TChatMessagesPagedResult> GetAll(Guid currentUserId, Guid chatId, IReadOnlyCollection<TMessageFilter> filters = default,
            TPagingOptions pagingOptions = default)
        {
            return await _chatMessagesService.GetAll(currentUserId, chatId, filters, pagingOptions);
        }

        public async Task Read(Guid currentUserId, Guid chatId, long index)
        {
            await _chatMessagesService.Read(currentUserId, chatId, index);
        }

        public async Task<Guid> Add(Guid currentUserId, Guid chatId, Guid? messageId, TChatMessageInfo messageInfo)
        {
            return await _chatMessagesService.Add(currentUserId, chatId, messageId, messageInfo);
        }

        public async Task<Guid> Edit(Guid currentUserId, Guid chatId, Guid messageId, TChatMessageInfo messageInfo, Guid? archivedMessageId)
        {
            return await _chatMessagesService.Edit(currentUserId, chatId, messageId, messageInfo, archivedMessageId);
        }

        public async Task Remove(Guid currentUserId, Guid chatId, Guid messageId)
        {
            await _chatMessagesService.Remove(currentUserId, chatId, messageId);
        }
    }
}
