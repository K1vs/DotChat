namespace K1vs.DotChat.Workers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using Commands.Chats;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using EventBuilders;
    using Events.Messages;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Security;
    using Stores.Chats;

    public class ChatsWorker<TChatWorkersConfiguration, TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, 
            TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> 
        : WorkerBase<TChatWorkersConfiguration>, 
        IChatsWorker<TChatInfo, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatWorkersConfiguration : IChatWorkersConfiguration
        where TChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection : IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatUser : IChatUser
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
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        private readonly IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> _chatsPermissionValidator;

        private readonly IChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> _chatStore;

        private readonly IChatsEventBuilder<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant> _chatsEventBuilder;

        protected ChatsWorker(TChatWorkersConfiguration chatWorkersConfiguration, IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> chatsPermissionValidator, IChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> chatStore, IChatsEventBuilder<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant> chatsEventBuilder) : base(chatWorkersConfiguration)
        {
            _chatsPermissionValidator = chatsPermissionValidator;
            _chatStore = chatStore;
            _chatsEventBuilder = chatsEventBuilder;
        }

        public async Task Handle(IAddChatCommand<TChatInfo, TParticipationCandidateCollection, TParticipationCandidate> command, IChatBusContext chatEventPublisher)
        {
            await _chatsPermissionValidator.ValidateAdd(command.InitiatorUserId, command.ChatId, command.ChatInfo, WorkerName).ConfigureAwait(false);
            var chat = await _chatStore.Create(command.ChatId, command.ChatInfo, command.ToAdd, command.ToInvite, command.InitiatorUserId).ConfigureAwait(false);
            var @event = _chatsEventBuilder.BuildChatAddedEvent(command.InitiatorUserId, chat);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IEditChatInfoCommand<TChatInfo> command, IChatBusContext chatEventPublisher)
        {
            await _chatsPermissionValidator.ValidateEditInfo(command.InitiatorUserId, command.ChatId, command.ChatInfo, WorkerName).ConfigureAwait(false);
            var chatInfo = await _chatStore.UpdateInfo(command.ChatId, command.ChatInfo, command.InitiatorUserId).ConfigureAwait(false);
            var @event = _chatsEventBuilder.BuildChatInfoEditedEvent(command.InitiatorUserId, command.ChatId, chatInfo);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IRemoveChatCommand command, IChatBusContext chatEventPublisher)
        {
            await _chatsPermissionValidator.ValidateRemove(command.InitiatorUserId, command.ChatId, WorkerName).ConfigureAwait(false);
            var chatInfo = await _chatStore.Delete(command.ChatId, command.InitiatorUserId).ConfigureAwait(false);
            var @event = _chatsEventBuilder.BuildChatRemovedEvent(command.InitiatorUserId, command.ChatId, chatInfo);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IChatMessageAddedEvent<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
        {
            await _chatStore.SetTop(@event.ChatId, @event.Message.Timestamp, @event.Message.Index);
        }
    }
}
