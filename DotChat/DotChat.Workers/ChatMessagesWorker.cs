namespace K1vs.DotChat.Workers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using Commands.Messages;
    using Common.Exceptions.Rules;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using EventBuilders;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Security;
    using Stores.Messages;

    public class ChatMessagesWorker<TChatWorkersConfiguration, TChatInfo, TChatUser, TChatMessageCollection, TChatMessage,  TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> : WorkerBase<TChatWorkersConfiguration>, 
        IChatMessagesWorker<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatWorkersConfiguration : IChatWorkersConfiguration
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
        private readonly IChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> _chatMessageStore;
        private readonly IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> _chatMessagesEventBuilder;

        protected ChatMessagesWorker(TChatWorkersConfiguration chatWorkersConfiguration, IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> chatMessagesPermissionValidator, IChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> chatMessageStore, IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesEventBuilder) : base(chatWorkersConfiguration)
        {
            _chatMessagesPermissionValidator = chatMessagesPermissionValidator;
            _chatMessageStore = chatMessageStore;
            _chatMessagesEventBuilder = chatMessagesEventBuilder;
        }

        public async Task Handle(IAddChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> command, IChatBusContext chatEventPublisher)
        {
            if (!command.IsSystem)
            {
                await _chatMessagesPermissionValidator.ValidateAdd(command.InitiatorUserId, command.ChatId, command.MessageInfo, WorkerName).ConfigureAwait(false);
            }
            var message = await _chatMessageStore.Create(command.ChatId, command.InitiatorUserId, command.MessageId, command.MessageInfo, command.Timestamp, command.Index, command.IsSystem, command.InitiatorUserId).ConfigureAwait(false);
            if (!ChatWorkersConfiguration.FastMessageMode)
            {
                var @event = _chatMessagesEventBuilder.BuildChatMessageAddedEvent(command.InitiatorUserId, command.ChatId, message);
                await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
            }
        }

        public async Task Handle(IEditChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> command, IChatBusContext chatEventPublisher)
        {
            var currentMessage = await _chatMessageStore.Retrieve(command.ChatId, command.MessageId).ConfigureAwait(false);
            if (!currentMessage.IsSystem)
            {
                await _chatMessagesPermissionValidator.ValidateEdit(command.InitiatorUserId, command.ChatId, command.MessageId, command.MessageInfo, WorkerName).ConfigureAwait(false);
            }
            if (currentMessage.Immutable)
            {
                throw new DotChatEditImmutableMessageException(command.MessageId, command.ChatId, command.InitiatorUserId);
            }
            var message = await _chatMessageStore.Update(command.ChatId, command.MessageId, command.MessageInfo,command.InitiatorUserId).ConfigureAwait(false);
            await _chatMessageStore.Archive(command.ChatId, currentMessage.MessageId,command.ArchivedMessageId, currentMessage, command.InitiatorUserId).ConfigureAwait(false);
            var @event = _chatMessagesEventBuilder.BuildChatMessageEditedEvent(command.InitiatorUserId, command.ChatId, message);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IRemoveChatMessageCommand command, IChatBusContext chatEventPublisher)
        {
            var currentMessage = await _chatMessageStore.Retrieve(command.ChatId, command.MessageId).ConfigureAwait(false);
            if (!currentMessage.IsSystem)
            {
                await _chatMessagesPermissionValidator.ValidateRemove(command.InitiatorUserId, command.ChatId, command.MessageId, WorkerName).ConfigureAwait(false);
            }
            if (currentMessage.Immutable)
            {
                throw new DotChatRemoveImmutableMessageException(command.MessageId, command.ChatId, command.InitiatorUserId);
            }
            var message = await _chatMessageStore.Delete(command.ChatId, command.MessageId, command.InitiatorUserId).ConfigureAwait(false);
            await _chatMessageStore.Archive(command.ChatId, currentMessage.MessageId, command.MessageId, currentMessage, command.InitiatorUserId).ConfigureAwait(false);
            var @event = _chatMessagesEventBuilder.BuildChatMessageRemovedEvent(command.InitiatorUserId, command.ChatId, message.MessageId, message.Version);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IReadChatMessagesCommand command, IChatBusContext chatEventPublisher)
        {
            await _chatMessagesPermissionValidator.ValidateRead(command.InitiatorUserId, command.ChatId, command.Index, command.Force, WorkerName).ConfigureAwait(false);
            await _chatMessageStore.Read(command.ChatId, command.InitiatorUserId, command.Index, command.Force).ConfigureAwait(false);
            var @event = _chatMessagesEventBuilder.BuildChatMessagesReadEvent(command.InitiatorUserId, command.ChatId, command.Index, command.Force);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }
    }
}
