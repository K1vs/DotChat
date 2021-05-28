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

    public class ChatMessagesService : ServiceBase, IChatMessagesService
    {
        protected readonly IChatMessagesPermissionValidator ChatMessagesPermissionValidator;
        protected readonly IReadChatMessageStore ReadChatMessageStore;
        protected readonly IChatMessagesCommandBuilder ChatMessagesCommandBuilder;
        protected readonly IChatCommandSender ChatCommandSender;

        public ChatMessagesService(IChatServicesConfiguration chatServicesConfiguration, IChatMessagesPermissionValidator chatMessagesPermissionValidator, IReadChatMessageStore readChatMessageStore, IChatMessagesCommandBuilder chatMessagesCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration)
        {
            ChatMessagesPermissionValidator = chatMessagesPermissionValidator;
            ReadChatMessageStore = readChatMessageStore;
            ChatMessagesCommandBuilder = chatMessagesCommandBuilder;
            ChatCommandSender = chatCommandSender;
        }

        public virtual async Task<IPagedResult<IChatMessage>> GetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<IChatMessageFilter> filters, IPagingOptions pagingOptions = default)
        {
            var messagesPage = await ReadChatMessageStore.Retrieve(chatId, filters, pagingOptions).ConfigureAwait(false);
            await ChatMessagesPermissionValidator.ValidateGetPage(currentUserId, chatId, filters, pagingOptions, messagesPage, ServiceName).ConfigureAwait(false);
            return messagesPage;
        }

        public virtual async Task<IPagedResult<IChatMessage>> GetPage(Guid currentUserId, Guid chatId, IPagingOptions pagingOptions = default)
        {
            var messagesPage = await ReadChatMessageStore.Retrieve(chatId, pagingOptions).ConfigureAwait(false);
            await ChatMessagesPermissionValidator.ValidateGetPage(currentUserId, chatId, pagingOptions, messagesPage, ServiceName).ConfigureAwait(false);
            return messagesPage;
        }

        public virtual async Task<Guid> Add(Guid currentUserId, Guid chatId, Guid? messageId, IChatMessageInfo messageInfo)
        {
            await ChatMessagesPermissionValidator.ValidateAdd(currentUserId, chatId, messageInfo, ServiceName)
                .ConfigureAwait(false);
            var command = ChatMessagesCommandBuilder.BuildIndexChatMessageCommand(currentUserId, chatId, messageId, false, messageInfo);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
            return command.MessageId;
        }

        public virtual async Task<Guid> Edit(Guid currentUserId, Guid chatId, Guid messageId, IChatMessageInfo messageInfo, Guid? archivedMessageId)
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
