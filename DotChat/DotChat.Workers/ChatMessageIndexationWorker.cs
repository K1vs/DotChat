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

    public class ChatMessageIndexationWorker<TChatWorkersConfiguration, TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> : WorkerBase<TChatWorkersConfiguration>, 
        IChatMessageIndexationWorker<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatWorkersConfiguration: IChatWorkersConfiguration
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
        private readonly IChatMessageTimestampGenerator _chatMessageTimestampGenerator;
        private readonly IChatMessageIndexGenerator _messageIndexGenerator;
        private readonly IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> _chatMessagesEventBuilder;
        private readonly IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> _chatMessagesCommandBuilder;

        protected ChatMessageIndexationWorker(TChatWorkersConfiguration chatWorkersConfiguration, IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> chatMessagesPermissionValidator, IChatMessageTimestampGenerator chatMessageTimestampGenerator, IChatMessageIndexGenerator messageIndexGenerator, IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesEventBuilder, IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesCommandBuilder) : base(chatWorkersConfiguration)
        {
            _chatMessagesPermissionValidator = chatMessagesPermissionValidator;
            _chatMessageTimestampGenerator = chatMessageTimestampGenerator;
            _messageIndexGenerator = messageIndexGenerator;
            _chatMessagesEventBuilder = chatMessagesEventBuilder;
            _chatMessagesCommandBuilder = chatMessagesCommandBuilder;
        }

        public async Task Handle(IIndexChatMessageCommand<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> command, IChatBusContext chatEventPublisher)
        {
            if (!command.IsSystem)
            {
                await _chatMessagesPermissionValidator.ValidateAdd(command.InitiatorUserId, command.ChatId, command.MessageInfo, WorkerName).ConfigureAwait(false);
            }
            var timestamp = await _chatMessageTimestampGenerator.Generate();
            var index = await _messageIndexGenerator.Generate(command.ChatId).ConfigureAwait(false);
            var addCommand = _chatMessagesCommandBuilder.BuildAddChatMessageCommand(command.InitiatorUserId, command.ChatId, command.MessageId, index, command.IsSystem, command.MessageInfo);
            await chatEventPublisher.CommandSender.Send(addCommand).ConfigureAwait(false);
            if (ChatWorkersConfiguration.FastMessageMode)
            {
                var @event = _chatMessagesEventBuilder.BuildChatMessageAddedEvent(command.InitiatorUserId, command.ChatId, command.MessageId, timestamp, index, command.IsSystem, command.MessageInfo);
                await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
            }
        }
    }
}
