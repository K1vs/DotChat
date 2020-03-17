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
        protected readonly IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, 
        TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> ChatMessagesPermissionValidator;
        protected readonly IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> ReadChatMessageStore;
        protected readonly IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> ChatMessagesCommandBuilder;
        protected readonly IChatCommandSender ChatCommandSender;

        public ChatMessagesService(TDotChatConfiguration chatServicesConfiguration, IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> chatMessagesPermissionValidator, IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> readChatMessageStore, IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration)
        {
            ChatMessagesPermissionValidator = chatMessagesPermissionValidator;
            ReadChatMessageStore = readChatMessageStore;
            ChatMessagesCommandBuilder = chatMessagesCommandBuilder;
            ChatCommandSender = chatCommandSender;
        }

        public virtual async Task<TPagedResult> GetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<TMessageFilter> filters, TPagingOptions pagingOptions = default)
        {
            var messagesPage = await ReadChatMessageStore.Retrieve(chatId, filters, pagingOptions).ConfigureAwait(false);
            await ChatMessagesPermissionValidator.ValidateGetPage(currentUserId, chatId, filters, pagingOptions, messagesPage, ServiceName).ConfigureAwait(false);
            return messagesPage;
        }

        public virtual async Task<TPagedResult> GetPage(Guid currentUserId, Guid chatId, TPagingOptions pagingOptions = default)
        {
            var messagesPage = await ReadChatMessageStore.Retrieve(chatId, pagingOptions).ConfigureAwait(false);
            await ChatMessagesPermissionValidator.ValidateGetPage(currentUserId, chatId, pagingOptions, messagesPage, ServiceName).ConfigureAwait(false);
            return messagesPage;
        }

        public virtual async Task<Guid> Add(Guid currentUserId, Guid chatId, Guid? messageId, TChatMessageInfo messageInfo)
        {
            await ChatMessagesPermissionValidator.ValidateAdd(currentUserId, chatId, messageInfo, ServiceName)
                .ConfigureAwait(false);
            var command = ChatMessagesCommandBuilder.BuildIndexChatMessageCommand(currentUserId, chatId, messageId, false, messageInfo);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
            return command.MessageId;
        }

        public virtual async Task<Guid> Edit(Guid currentUserId, Guid chatId, Guid messageId, TChatMessageInfo messageInfo, Guid? archivedMessageId)
        {
            await ChatMessagesPermissionValidator.ValidateEdit(currentUserId, chatId, messageId, messageInfo, ServiceName)
                .ConfigureAwait(false);
            var command = ChatMessagesCommandBuilder.BuildEditChatMessageCommand(currentUserId, chatId, messageId, messageInfo, archivedMessageId);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
            return command.ArchivedMessageId;
        }

        public virtual async Task Remove(Guid currentUserId, Guid chatId, Guid messageId)
        {
            await ChatMessagesPermissionValidator.ValidateRemove(currentUserId, chatId, messageId, ServiceName)
                .ConfigureAwait(false);
            var command = ChatMessagesCommandBuilder.BuildRemoveChatMessageCommand(currentUserId, chatId, messageId);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }

        public virtual async Task Read(Guid currentUserId, Guid chatId, long index, bool force)
        {
            await ChatMessagesPermissionValidator.ValidateRead(currentUserId, chatId, index, force, ServiceName)
                .ConfigureAwait(false);
            var command = ChatMessagesCommandBuilder.BuildReadChatMessagesCommand(currentUserId, chatId, index, force);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }
    }
}
