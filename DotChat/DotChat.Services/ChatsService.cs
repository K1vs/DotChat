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

    public class ChatsService<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> : ServiceBase<TDotChatConfiguration>, 
        IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions>
        where TDotChatConfiguration : IChatServicesConfiguration
        where TPersonalizedChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection : IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat: IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidates : IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatUser : IChatUser
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        protected readonly IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> ChatsPermissionValidator;
        protected readonly IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> ReadChatStore;
        protected readonly IChatsCommandBuilder<TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate> ChatsCommandBuilder;
        protected readonly IChatCommandSender ChatCommandSender;

        public ChatsService(TDotChatConfiguration chatServicesConfiguration, IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> chatsPermissionValidator,
            IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> readChatStore, 
            IChatsCommandBuilder<TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate> chatsCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration)
        {
            ChatsPermissionValidator = chatsPermissionValidator;
            ReadChatStore = readChatStore;
            ChatsCommandBuilder = chatsCommandBuilder;
            ChatCommandSender = chatCommandSender;
        }

        public virtual async Task<TPersonalizedChatsSummary> GetSummary(Guid currentUserId)
        {
            await ChatsPermissionValidator.ValidateGetSummary(currentUserId, ServiceName);
            return await ReadChatStore.RetrievePersonalizedSummary(currentUserId);
        }

        public virtual async Task<TPersonalizedChat> Get(Guid currentUserId, Guid chatId)
        {
            var chat = await ReadChatStore.RetrievePersonalized(chatId, currentUserId);
            await ChatsPermissionValidator.ValidateGet(currentUserId, chat, ServiceName);
            return chat;
        }

        public virtual async Task<TPagedResult> GetPage(Guid currentUserId, TChatFilter filter, TPagingOptions pagingOptions = default)
        {
            var chatsPage = await ReadChatStore.RetrievePersonalizedPage(currentUserId, filter, pagingOptions).ConfigureAwait(false);
            await ChatsPermissionValidator.ValidateGetPage(currentUserId, chatsPage, filter, pagingOptions, ServiceName).ConfigureAwait(false);
            return chatsPage;
        }

        public virtual async Task<TPagedResult> GetPage(Guid currentUserId, TPagingOptions pagingOptions = default)
        {
            var chatsPage = await ReadChatStore.RetrievePersonalizedPage(currentUserId, pagingOptions).ConfigureAwait(false);
            await ChatsPermissionValidator.ValidateGetPage(currentUserId, chatsPage, pagingOptions, ServiceName).ConfigureAwait(false);
            return chatsPage;
        }

        public virtual async Task<Guid> Add(Guid currentUserId, Guid? chatId, TChatInfo chatInfo, TParticipationCandidates participationCandidates)
        {
            var command = ChatsCommandBuilder.BuildAddChatCommand(currentUserId, chatId, chatInfo, participationCandidates);
            await ChatsPermissionValidator.ValidateAdd(currentUserId, command.ChatId, chatInfo, ServiceName).ConfigureAwait(false);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
            return command.ChatId;
        }

        public virtual async Task EditInfo(Guid currentUserId, Guid chatId, TChatInfo chatInfo)
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
