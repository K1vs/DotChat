namespace K1vs.DotChat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using CommandBuilders;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Security;
    using Stores.Messages;

    public class ChatMessagesService<TDotChatConfiguration, TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> : ServiceBase<TDotChatConfiguration>, 
        IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions>
        where TDotChatConfiguration : IChatServicesConfiguration
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
        private readonly IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, 
        TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> _chatMessagesPermissionValidator;
        private readonly IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> _readChatMessageStore;
        private readonly IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> _chatMessagesCommandBuilder;
        private readonly IChatCommandSender _chatCommandSender;

        public ChatMessagesService(TDotChatConfiguration chatServicesConfiguration, IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> chatMessagesPermissionValidator, IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> readChatMessageStore, IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration)
        {
            _chatMessagesPermissionValidator = chatMessagesPermissionValidator;
            _readChatMessageStore = readChatMessageStore;
            _chatMessagesCommandBuilder = chatMessagesCommandBuilder;
            _chatCommandSender = chatCommandSender;
        }

        public async Task<TPagedResult> GetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<TMessageFilter> filters, TPagingOptions pagingOptions = default)
        {
            var messagesPage = await _readChatMessageStore.Retrieve(chatId, filters, pagingOptions).ConfigureAwait(false);
            await _chatMessagesPermissionValidator.ValidateGetPage(currentUserId, chatId, filters, pagingOptions, messagesPage, ServiceName).ConfigureAwait(false);
            return messagesPage;
        }

        public async Task<TPagedResult> GetPage(Guid currentUserId, Guid chatId, TPagingOptions pagingOptions = default)
        {
            var messagesPage = await _readChatMessageStore.Retrieve(chatId, pagingOptions).ConfigureAwait(false);
            await _chatMessagesPermissionValidator.ValidateGetPage(currentUserId, chatId, pagingOptions, messagesPage, ServiceName).ConfigureAwait(false);
            return messagesPage;
        }

        public async Task<Guid> Add(Guid currentUserId, Guid chatId, Guid? messageId, TChatMessageInfo messageInfo)
        {
            await _chatMessagesPermissionValidator.ValidateAdd(currentUserId, chatId, messageInfo, ServiceName)
                .ConfigureAwait(false);
            var command = _chatMessagesCommandBuilder.BuildIndexChatMessageCommand(currentUserId, chatId, messageId, false, messageInfo);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
            return command.MessageId;
        }

        public async Task<Guid> Edit(Guid currentUserId, Guid chatId, Guid messageId, TChatMessageInfo messageInfo, Guid? archivedMessageId)
        {
            await _chatMessagesPermissionValidator.ValidateEdit(currentUserId, chatId, messageId, messageInfo, ServiceName)
                .ConfigureAwait(false);
            var command = _chatMessagesCommandBuilder.BuildEditChatMessageCommand(currentUserId, chatId, messageId, messageInfo, archivedMessageId);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
            return command.ArchivedMessageId;
        }

        public async Task Remove(Guid currentUserId, Guid chatId, Guid messageId)
        {
            await _chatMessagesPermissionValidator.ValidateRemove(currentUserId, chatId, messageId, ServiceName)
                .ConfigureAwait(false);
            var command = _chatMessagesCommandBuilder.BuildRemoveChatMessageCommand(currentUserId, chatId, messageId);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }

        public async Task Read(Guid currentUserId, Guid chatId, long index)
        {
            await _chatMessagesPermissionValidator.ValidateRead(currentUserId, chatId, index, ServiceName)
                .ConfigureAwait(false);
            var command = _chatMessagesCommandBuilder.BuildReadChatMessagesCommand(currentUserId, chatId, index);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }
    }
}
