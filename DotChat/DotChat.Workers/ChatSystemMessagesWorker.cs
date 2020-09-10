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

    public class ChatSystemMessagesWorker: WorkerBase, IChatSystemMessagesWorker
    {
        protected readonly ISystemMessagesBuilder SystemMessagesBuilder;

        protected readonly IChatMessagesCommandBuilder ChatMessagesCommandBuilder;


        protected ChatSystemMessagesWorker(IChatWorkersConfiguration chatWorkersConfiguration, ISystemMessagesBuilder systemMessagesBuilder, IChatMessagesCommandBuilder chatMessagesCommandBuilder) : base(chatWorkersConfiguration)
        {
            SystemMessagesBuilder = systemMessagesBuilder;
            ChatMessagesCommandBuilder = chatMessagesCommandBuilder;
        }

        public virtual async Task Handle(IChatParticipantAddedEvent @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantAddedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantInvitedEvent @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantInvitedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantAppliedEvent @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantAppliedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantRemovedEvent @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantRemovedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantBlockedEvent @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatParticipantBlockedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        public virtual async Task Handle(IChatParticipantsAppendedEvent @event, IChatBusContext chatBusContext)
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

        public virtual async Task Handle(IChatAddedEvent @event, IChatBusContext chatBusContext)
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

        public virtual async Task Handle(IChatInfoEditedEvent @event, IChatBusContext chatBusContext)
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            var messageInfo = SystemMessagesBuilder.BuildChatInfoEditedMessage(@event);
            await AddMessage(@event, messageInfo, chatBusContext);
        }

        protected virtual async Task AddMessage<TEvent>(TEvent @event, IChatMessageInfo messageInfo, IChatBusContext chatBusContext)
            where TEvent : IEvent, IChatRelated
        {
            if (ChatWorkersConfiguration.DisableSystemMessages)
            {
                return;
            }
            await AddMessage(@event.InitiatorUserId, @event.ChatId, messageInfo, chatBusContext);
        }

        protected virtual async Task AddMessage(Guid initiatorUserId, Guid chatId, IChatMessageInfo messageInfo, IChatBusContext chatBusContext)
        {
            if (messageInfo != null)
            {
                var addMessageCommand = ChatMessagesCommandBuilder.BuildIndexChatMessageCommand(initiatorUserId, chatId, null, true, messageInfo);
                await chatBusContext.CommandSender.Send(addMessageCommand);
            }
        }
    }
}
