namespace K1vs.DotChat.Workers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using Commands.Messages;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using EventBuilders;
    using K1vs.DotChat.Exceptions;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Security;
    using Stores.Messages;

    public class ChatMessagesWorker: WorkerBase, IChatMessagesWorker
    {
        protected readonly IChatMessagesPermissionValidator ChatMessagesPermissionValidator;
        protected readonly IChatMessageStore ChatMessageStore;
        protected readonly IChatMessagesEventBuilder ChatMessagesEventBuilder;

        protected ChatMessagesWorker(IChatWorkersConfiguration chatWorkersConfiguration, IChatMessagesPermissionValidator chatMessagesPermissionValidator, IChatMessageStore chatMessageStore, IChatMessagesEventBuilder chatMessagesEventBuilder) : base(chatWorkersConfiguration)
        {
            ChatMessagesPermissionValidator = chatMessagesPermissionValidator;
            ChatMessageStore = chatMessageStore;
            ChatMessagesEventBuilder = chatMessagesEventBuilder;
        }

        public virtual async Task Handle(IAddChatMessageCommand command, IChatBusContext chatEventPublisher)
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

        public virtual async Task Handle(IEditChatMessageCommand command, IChatBusContext chatEventPublisher)
        {
            var currentMessage = await ChatMessageStore.Retrieve(command.ChatId, command.MessageId).ConfigureAwait(false);
            if (!currentMessage.IsSystem)
            {
                await ChatMessagesPermissionValidator.ValidateEdit(command.InitiatorUserId, command.ChatId, command.MessageId, command.MessageInfo, WorkerName).ConfigureAwait(false);
            }
            if (currentMessage.Immutable)
            {
                throw new DotChatException(ExceptionCode.RulesViolationEditImmutable, new {
                    command.MessageId,
                    command.ChatId,
                    command.InitiatorUserId
                });
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
                throw new DotChatException(ExceptionCode.RulesViolationEditImmutable, new
                {
                    command.MessageId,
                    command.ChatId,
                    command.InitiatorUserId
                });
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
