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
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;
    using Security;
    using Stores.Chats;

    public class ChatsService: ServiceBase, IChatsService
    {
        protected readonly IChatsPermissionValidator ChatsPermissionValidator;
        protected readonly IReadChatStore ReadChatStore;
        protected readonly IChatsCommandBuilder ChatsCommandBuilder;
        protected readonly IChatCommandSender ChatCommandSender;

        public ChatsService(IChatServicesConfiguration chatServicesConfiguration, IChatsPermissionValidator chatsPermissionValidator, IReadChatStore readChatStore, IChatsCommandBuilder chatsCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration)
        {
            ChatsPermissionValidator = chatsPermissionValidator;
            ReadChatStore = readChatStore;
            ChatsCommandBuilder = chatsCommandBuilder;
            ChatCommandSender = chatCommandSender;
        }

        public virtual async Task<IPersonalizedChatsSummary> GetSummary(Guid currentUserId)
        {
            await ChatsPermissionValidator.ValidateGetSummary(currentUserId, ServiceName);
            return await ReadChatStore.RetrievePersonalizedSummary(currentUserId);
        }

        public virtual async Task<IPersonalizedChat> Get(Guid currentUserId, Guid chatId)
        {
            var chat = await ReadChatStore.RetrievePersonalized(chatId, currentUserId);
            await ChatsPermissionValidator.ValidateGet(currentUserId, chat, ServiceName);
            return chat;
        }

        public virtual async Task<IPagedResult<IPersonalizedChat>> GetPage(Guid currentUserId, IChatFilter filter, IPagingOptions pagingOptions = default)
        {
            var chatsPage = await ReadChatStore.RetrievePersonalizedPage(currentUserId, filter, pagingOptions).ConfigureAwait(false);
            await ChatsPermissionValidator.ValidateGetPage(currentUserId, chatsPage, filter, pagingOptions, ServiceName).ConfigureAwait(false);
            return chatsPage;
        }

        public virtual async Task<IPagedResult<IPersonalizedChat>> GetPage(Guid currentUserId, IPagingOptions pagingOptions = default)
        {
            var chatsPage = await ReadChatStore.RetrievePersonalizedPage(currentUserId, pagingOptions).ConfigureAwait(false);
            await ChatsPermissionValidator.ValidateGetPage(currentUserId, chatsPage, pagingOptions, ServiceName).ConfigureAwait(false);
            return chatsPage;
        }

        public virtual async Task<Guid> Add(Guid currentUserId, Guid? chatId, IChatInfo chatInfo, IParticipantsAddInviteBulk participationCandidates)
        {
            var command = ChatsCommandBuilder.BuildAddChatCommand(currentUserId, chatId, chatInfo, participationCandidates);
            await ChatsPermissionValidator.ValidateAdd(currentUserId, command.ChatId, chatInfo, ServiceName).ConfigureAwait(false);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
            return command.ChatId;
        }

        public virtual async Task EditInfo(Guid currentUserId, Guid chatId, IChatInfo chatInfo)
        {
            await ChatsPermissionValidator.ValidateEditInfo(currentUserId, chatId, chatInfo, ServiceName).ConfigureAwait(false);
            var command = ChatsCommandBuilder.BuildEditChatCommand(currentUserId, chatId, chatInfo);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }

        public virtual async Task Remove(Guid currentUserId, Guid chatId)
        {
            await ChatsPermissionValidator.ValidateRemove(currentUserId, chatId, ServiceName);
            var command = ChatsCommandBuilder.BuildRemoveChatCommand(currentUserId, chatId);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }
    }
}
