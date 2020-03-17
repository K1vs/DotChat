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
        protected readonly ISystemMessagesBuilder<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult,
            TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
            TContactMessageCollection, TContactMessage> SystemMessagesBuilder;

        protected readonly IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> ChatMessagesCommandBuilder;


        protected ChatSystemMessagesWorker(TChatWorkersConfiguration chatWorkersConfiguration, ISystemMessagesBuilder<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> systemMessagesBuilder, IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessagesCommandBuilder) : base(chatWorkersConfiguration)
        {
            SystemMessagesBuilder = systemMessagesBuilder;
            ChatMessagesCommandBuilder = chatMessagesCommandBuilder;
        }

        public virtual async Task Handle(IChatParticipantAddedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantAddedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantInvitedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantInvitedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantAppliedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantAppliedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantRemovedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantRemovedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantBlockedEvent<TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantBlockedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messagesInfo = SystemMessagesBuilder.BuildBulkParticipantsAppendedMessages(@event);
            foreach (var messageInfo in messagesInfo)
            {
                await AddMessage(@event, messageInfo, chatBusContext);
            }
        }

        public virtual async Task Handle(IChatAddedEvent<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messagesInfo = SystemMessagesBuilder.BuildChatAddedMessage(@event);
            foreach (var messageInfo in messagesInfo)
            {
                await AddMessage(@event.InitiatorUserId, @event.Chat.ChatId, messageInfo, chatBusContext);
            }
        }

        public virtual async Task Handle(IChatInfoEditedEvent<TChatInfo> @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatInfoEditedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        protected virtual async Task AddMessage<TEvent>(TEvent @event, TChatMessageInfo messageInfo, IChatBusContext chatBusContext)
            where TEvent : IEvent, IChatRelated
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            await AddMessage(@event.InitiatorUserId, @event.ChatId, messageInfo, chatBusContext);
        }

        protected virtual async Task AddMessage(Guid initiatorUserId, Guid chatId, TChatMessageInfo messageInfo, IChatBusContext chatBusContext)
        {
            if (messageInfo != null)
            {
                var addMessageCommand = ChatMessagesCommandBuilder.BuildIndexChatMessageCommand(initiatorUserId, chatId, null, true, messageInfo);
                await chatBusContext.CommandSender.Send(addMessageCommand);
            }
        }
    }
}
