namespace K1vs.DotChat.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SystemMessages;
    using Bus;
    using Chats;
    using CommandBuilders;
    using Configuration;
    using Events;
    using Events.Chats;
    using Events.Participants;
    using Messages;
    using Messages.Typed;
    using Participants;

    public class ChatSystemMessagesWorker<TChatWorkersConfiguration, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> : WorkerBase<TChatWorkersConfiguration>,
        IChatSystemMessagesWorker<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatWorkersConfiguration : IChatWorkersConfiguration
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
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
    {
        private readonly ISystemMessagesBuilder<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult,
            TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
            TContactMessageCollection, TContactMessage> _systemMessagesBuilder;

        private readonly IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> _chatMessagesCommandBuilder;


        protected ChatSystemMessagesWorker(TChatWorkersConfiguration chatWorkersConfiguration, ISystemMessagesBuilder<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> systemMessagesBuilder, IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesCommandBuilder) : base(chatWorkersConfiguration)
        {
            _systemMessagesBuilder = systemMessagesBuilder;
            _chatMessagesCommandBuilder = chatMessagesCommandBuilder;
        }

        public async Task Handle(IChatParticipantAddedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = _systemMessagesBuilder.BuildChatParticipantAddedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public async Task Handle(IChatParticipantInvitedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = _systemMessagesBuilder.BuildChatParticipantInvitedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public async Task Handle(IChatParticipantAppliedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = _systemMessagesBuilder.BuildChatParticipantAppliedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public async Task Handle(IChatParticipantRemovedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = _systemMessagesBuilder.BuildChatParticipantRemovedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public async Task Handle(IChatParticipantBlockedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = _systemMessagesBuilder.BuildChatParticipantBlockedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public async Task Handle(IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messagesInfo = _systemMessagesBuilder.BuildBulkParticipantsAppendedMessages(@event);
            foreach (var messageInfo in messagesInfo)
            {
                await AddMessage(@event, messageInfo, chatBusContext);
            }
        }

        public async Task Handle(IChatAddedEvent<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messagesInfo = _systemMessagesBuilder.BuildChatAddedMessage(@event);
            foreach (var messageInfo in messagesInfo)
            {
                await AddMessage(@event.InitiatorUserId, @event.Chat.ChatId, messageInfo, chatBusContext);
            }
        }

        public async Task Handle(IChatInfoEditedEvent<TChatInfo> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = _systemMessagesBuilder.BuildChatInfoEditedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        private async Task AddMessage<TEvent>(TEvent @event, TChatMessageInfo messageInfo, IChatBusContext chatBusContext)
            where TEvent : IEvent, IChatRelated
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            await AddMessage(@event.InitiatorUserId, @event.ChatId, messageInfo, chatBusContext);
        }

        private async Task AddMessage(Guid initiatorUserId, Guid chatId, TChatMessageInfo messageInfo, IChatBusContext chatBusContext)
        {
            if (messageInfo != null)
            {
                var addMessageCommand = _chatMessagesCommandBuilder.BuildIndexChatMessageCommand(initiatorUserId, chatId, null, true, messageInfo);
                await chatBusContext.CommandSender.Send(addMessageCommand);
            }
        }
    }
}
