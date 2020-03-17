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
        protected readonly IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, 
        TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> ChatMessagesPermissionValidator;
        protected readonly IChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> ChatMessageStore;
        protected readonly IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> ChatMessagesEventBuilder;

        protected ChatMessagesWorker(TChatWorkersConfiguration chatWorkersConfiguration, IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> chatMessagesPermissionValidator, IChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> chatMessageStore, IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesEventBuilder) : base(chatWorkersConfiguration)
        {
            ChatMessagesPermissionValidator = chatMessagesPermissionValidator;
            ChatMessageStore = chatMessageStore;
            ChatMessagesEventBuilder = chatMessagesEventBuilder;
        }

        public virtual async Task Handle(IAddChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> command, IChatBusContext chatEventPublisher)
        {
            if (!command.IsSystem)
            {
                await ChatMessagesPermissionValidator.ValidateAdd(command.InitiatorUserId, command.ChatId, command.MessageInfo, WorkerName).ConfigureAwait(false);
            }
            var message = await ChatMessageStore.Create(command.ChatId, command.InitiatorUserId, command.MessageId, command.MessageInfo, command.Timestamp, command.Index, command.IsSystem, command.InitiatorUserId).ConfigureAwait(false);
            if (!ChatWorkersConfiguration.FastMessageMode)
            {
                var @event = ChatMessagesEventBuilder.BuildChatMessageAddedEvent(command.InitiatorUserId, command.ChatId, message);
                await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
            }
        }

        public virtual async Task Handle(IEditChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> command, IChatBusContext chatEventPublisher)
        {
            var currentMessage = await ChatMessageStore.Retrieve(command.ChatId, command.MessageId).ConfigureAwait(false);
            if (!currentMessage.IsSystem)
            {
                await ChatMessagesPermissionValidator.ValidateEdit(command.InitiatorUserId, command.ChatId, command.MessageId, command.MessageInfo, WorkerName).ConfigureAwait(false);
            }
            if (currentMessage.Immutable)
            {
                throw new DotChatEditImmutableMessageException(command.MessageId, command.ChatId, command.InitiatorUserId);
            }
            var message = await ChatMessageStore.Update(command.ChatId, command.MessageId, command.MessageInfo,command.InitiatorUserId).ConfigureAwait(false);
            await ChatMessageStore.Archive(command.ChatId, currentMessage.MessageId,command.ArchivedMessageId, currentMessage, command.InitiatorUserId).ConfigureAwait(false);
            var @event = ChatMessagesEventBuilder.BuildChatMessageEditedEvent(command.InitiatorUserId, command.ChatId, message);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IRemoveChatMessageCommand command, IChatBusContext chatEventPublisher)
        {
            var currentMessage = await ChatMessageStore.Retrieve(command.ChatId, command.MessageId).ConfigureAwait(false);
            if (!currentMessage.IsSystem)
            {
                await ChatMessagesPermissionValidator.ValidateRemove(command.InitiatorUserId, command.ChatId, command.MessageId, WorkerName).ConfigureAwait(false);
            }
            if (currentMessage.Immutable)
            {
                throw new DotChatRemoveImmutableMessageException(command.MessageId, command.ChatId, command.InitiatorUserId);
            }
            var message = await ChatMessageStore.Delete(command.ChatId, command.MessageId, command.InitiatorUserId).ConfigureAwait(false);
            await ChatMessageStore.Archive(command.ChatId, currentMessage.MessageId, command.MessageId, currentMessage, command.InitiatorUserId).ConfigureAwait(false);
            var @event = ChatMessagesEventBuilder.BuildChatMessageRemovedEvent(command.InitiatorUserId, command.ChatId, message);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IReadChatMessagesCommand command, IChatBusContext chatEventPublisher)
        {
            await ChatMessagesPermissionValidator.ValidateRead(command.InitiatorUserId, command.ChatId, command.Index, command.Force, WorkerName).ConfigureAwait(false);
            await ChatMessageStore.Read(command.ChatId, command.InitiatorUserId, command.Index, command.Force).ConfigureAwait(false);
            var @event = ChatMessagesEventBuilder.BuildChatMessagesReadEvent(command.InitiatorUserId, command.ChatId, command.Index, command.Force);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }
    }
}
