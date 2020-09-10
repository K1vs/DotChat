namespace K1vs.DotChat.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using CommandBuilders;
    using Commands.Messages;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using EventBuilders;
    using Generators;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Security;

    public class ChatMessageIndexationWorker: WorkerBase, IChatMessageIndexationWorker
    {
        protected readonly IChatMessagesPermissionValidator ChatMessagesPermissionValidator;
        protected readonly IChatMessageTimestampGenerator ChatMessageTimestampGenerator;
        protected readonly IChatMessageIndexGenerator _messageIndexGenerator;
        protected readonly IChatMessagesEventBuilder ChatMessagesEventBuilder;
        protected readonly IChatMessagesCommandBuilder ChatMessagesCommandBuilder;

        protected ChatMessageIndexationWorker(IChatWorkersConfiguration chatWorkersConfiguration, IChatMessagesPermissionValidator chatMessagesPermissionValidator, IChatMessageTimestampGenerator chatMessageTimestampGenerator, IChatMessageIndexGenerator messageIndexGenerator, IChatMessagesEventBuilder chatMessagesEventBuilder, IChatMessagesCommandBuilder chatMessagesCommandBuilder)
            : base(chatWorkersConfiguration)
        {
            ChatMessagesPermissionValidator = chatMessagesPermissionValidator;
            ChatMessageTimestampGenerator = chatMessageTimestampGenerator;
            _messageIndexGenerator = messageIndexGenerator;
            ChatMessagesEventBuilder = chatMessagesEventBuilder;
            ChatMessagesCommandBuilder = chatMessagesCommandBuilder;
        }

        public virtual async Task Handle(IIndexChatMessageCommand command, IChatBusContext chatEventPublisher)
        {
            if (!command.IsSystem)
            {
                await ChatMessagesPermissionValidator.ValidateAdd(command.InitiatorUserId, command.ChatId, command.MessageInfo, WorkerName).ConfigureAwait(false);
            }
            var timestamp = await ChatMessageTimestampGenerator.Generate();
            var index = await _messageIndexGenerator.Generate(command.ChatId).ConfigureAwait(false);
            var addCommand = ChatMessagesCommandBuilder.BuildAddChatMessageCommand(command.InitiatorUserId, command.ChatId, command.MessageId, timestamp, index, command.IsSystem, command.MessageInfo);
            await chatEventPublisher.CommandSender.Send(addCommand).ConfigureAwait(false);
            if (ChatWorkersConfiguration.FastMessageMode)
            {
                var @event = ChatMessagesEventBuilder.BuildChatMessageAddedEvent(command.InitiatorUserId, command.ChatId, command.MessageId, timestamp, index, command.IsSystem, command.MessageInfo);
                await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
            }
        }
    }
}
