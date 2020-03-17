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
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
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
        protected readonly IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> ChatsPermissionValidator;

        protected readonly IChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> ChatStore;

        protected readonly IChatsEventBuilder<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> ChatsEventBuilder;

        protected ChatsWorker(TChatWorkersConfiguration chatWorkersConfiguration, IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage,
            TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> chatsPermissionValidator, 
            IChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> chatStore, IChatsEventBuilder<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatsEventBuilder) : base(chatWorkersConfiguration)
        {
            ChatsPermissionValidator = chatsPermissionValidator;
            ChatStore = chatStore;
            ChatsEventBuilder = chatsEventBuilder;
        }

        public virtual async Task Handle(IAddChatCommand<TChatInfo, TParticipationCandidateCollection, TParticipationCandidate> command, IChatBusContext chatEventPublisher)
        {
            await ChatsPermissionValidator.ValidateAdd(command.InitiatorUserId, command.ChatId, command.ChatInfo, WorkerName).ConfigureAwait(false);
            var chat = await ChatStore.Create(command.ChatId, command.ChatInfo, command.ToAdd, command.ToInvite, command.InitiatorUserId).ConfigureAwait(false);
            var @event = ChatsEventBuilder.BuildChatAddedEvent(command.InitiatorUserId, chat);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IEditChatInfoCommand<TChatInfo> command, IChatBusContext chatEventPublisher)
        {
            await ChatsPermissionValidator.ValidateEditInfo(command.InitiatorUserId, command.ChatId, command.ChatInfo, WorkerName).ConfigureAwait(false);
            var chatInfo = await ChatStore.UpdateInfo(command.ChatId, command.ChatInfo, command.InitiatorUserId).ConfigureAwait(false);
            var @event = ChatsEventBuilder.BuildChatInfoEditedEvent(command.InitiatorUserId, command.ChatId, chatInfo);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IRemoveChatCommand command, IChatBusContext chatEventPublisher)
        {
            await ChatsPermissionValidator.ValidateRemove(command.InitiatorUserId, command.ChatId, WorkerName).ConfigureAwait(false);
            var chatInfo = await ChatStore.Delete(command.ChatId, command.InitiatorUserId).ConfigureAwait(false);
            var @event = ChatsEventBuilder.BuildChatRemovedEvent(command.InitiatorUserId, command.ChatId, chatInfo);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IChatMessageAddedEvent<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
        {
            await ChatStore.SetTop(@event.ChatId, @event.Message);
        }
    }
}
