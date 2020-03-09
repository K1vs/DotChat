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
    using Participants;
    using Security;
    using Stores.Chats;

    public class ChatsService<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> : ServiceBase<TDotChatConfiguration>, 
        IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions>
        where TDotChatConfiguration : IChatServicesConfiguration
        where TPersonalizedChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection : IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChat: IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidates : IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        private readonly IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> _chatsPermissionValidator;
        private readonly IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> _readChatStore;
        private readonly IChatsCommandBuilder<TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate> _chatsCommandBuilder;
        private readonly IChatCommandSender _chatCommandSender;

        public ChatsService(TDotChatConfiguration chatServicesConfiguration, IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> chatsPermissionValidator, IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> readChatStore, IChatsCommandBuilder<TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate> chatsCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration)
        {
            _chatsPermissionValidator = chatsPermissionValidator;
            _readChatStore = readChatStore;
            _chatsCommandBuilder = chatsCommandBuilder;
            _chatCommandSender = chatCommandSender;
        }

        public async Task<TPersonalizedChatsSummary> GetSummary(Guid currentUserId)
        {
            await _chatsPermissionValidator.ValidateGetSummary(currentUserId, ServiceName);
            return await _readChatStore.RetrievePersonalizedSummary(currentUserId);
        }

        public async Task<TPersonalizedChat> Get(Guid currentUserId, Guid chatId)
        {
            var chat = await _readChatStore.RetrievePersonalized(chatId, currentUserId);
            await _chatsPermissionValidator.ValidateGet(currentUserId, chat, ServiceName);
            return chat;
        }

        public async Task<TPagedResult> GetPage(Guid currentUserId, TChatFilter filter, TPagingOptions pagingOptions = default)
        {
            var chatsPage = await _readChatStore.RetrievePersonalizedPage(currentUserId, filter, pagingOptions).ConfigureAwait(false);
            await _chatsPermissionValidator.ValidateGetPage(currentUserId, chatsPage, filter, pagingOptions, ServiceName).ConfigureAwait(false);
            return chatsPage;
        }

        public async Task<TPagedResult> GetPage(Guid currentUserId, TPagingOptions pagingOptions = default)
        {
            var chatsPage = await _readChatStore.RetrievePersonalizedPage(currentUserId, pagingOptions).ConfigureAwait(false);
            await _chatsPermissionValidator.ValidateGetPage(currentUserId, chatsPage, pagingOptions, ServiceName).ConfigureAwait(false);
            return chatsPage;
        }

        public async Task<Guid> Add(Guid currentUserId, TChatInfo chatInfo, TParticipationCandidates participationCandidates)
        {
            var command = _chatsCommandBuilder.BuildAddChatCommand(currentUserId, chatInfo, participationCandidates);
            await _chatsPermissionValidator.ValidateAdd(currentUserId, command.ChatId, chatInfo, ServiceName).ConfigureAwait(false);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
            return command.ChatId;
        }

        public async Task EditInfo(Guid currentUserId, Guid chatId, TChatInfo chatInfo)
        {
            await _chatsPermissionValidator.ValidateEditInfo(currentUserId, chatId, chatInfo, ServiceName).ConfigureAwait(false);
            var command = _chatsCommandBuilder.BuildEditChatCommand(currentUserId, chatId, chatInfo);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }

        public async Task Remove(Guid currentUserId, Guid chatId)
        {
            await _chatsPermissionValidator.ValidateRemove(currentUserId, chatId, ServiceName);
            var command = _chatsCommandBuilder.BuildRemoveChatCommand(currentUserId, chatId);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }
    }
}
