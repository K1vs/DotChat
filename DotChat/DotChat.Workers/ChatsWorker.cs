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

    public class ChatsWorker: WorkerBase, IChatsWorker
    {
        protected readonly IChatsPermissionValidator ChatsPermissionValidator;

        protected readonly IChatStore ChatStore;

        protected readonly IChatsEventBuilder ChatsEventBuilder;

        protected ChatsWorker(IChatWorkersConfiguration chatWorkersConfiguration, IChatsPermissionValidator chatsPermissionValidator, IChatStore chatStore, IChatsEventBuilder chatsEventBuilder) : base(chatWorkersConfiguration)
        {
            ChatsPermissionValidator = chatsPermissionValidator;
            ChatStore = chatStore;
            ChatsEventBuilder = chatsEventBuilder;
        }

        public virtual async Task Handle(IAddChatCommand command, IChatBusContext chatEventPublisher)
        {
            await ChatsPermissionValidator.ValidateAdd(command.InitiatorUserId, command.ChatId, command.ChatInfo, WorkerName).ConfigureAwait(false);
            var chat = await ChatStore.Create(command.ChatId, command.ChatInfo, command.ToAdd, command.ToInvite, command.InitiatorUserId).ConfigureAwait(false);
            var @event = ChatsEventBuilder.BuildChatAddedEvent(command.InitiatorUserId, chat);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IEditChatInfoCommand command, IChatBusContext chatEventPublisher)
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

        public virtual async Task Handle(IChatMessageAddedEvent @event, IChatBusContext chatBusContext)
        {
            await ChatStore.SetTop(@event.ChatId, @event.Message);
        }
    }
}
