namespace K1vs.DotChat.Modules
{
    using System.Collections.Generic;
    using Bus;
    using Chats;
    using CommandBuilders;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using Dependency;
    using EventBuilders;
    using Messages;
    using Messages.Typed;
    using NotificationBuilders;
    using Notifiers;
    using Participants;
    using Security;
    using Services;
    using Stores.Chats;
    using Stores.Messages;
    using Stores.Participants;
    using Stores.Users;

    public abstract class ChatNotificationModule: IChatNotificationModule
    {
        protected readonly ChatServiceModule ChatServiceModule;

        protected ChatNotificationModule(ChatServiceModule chatServiceModule)
        {
            ChatServiceModule = chatServiceModule;
        }

        public virtual void Register(IDependencyRegistrar registrar, bool rootModule)
        {
            if (ChatServiceModule != null)
            {
                ChatServiceModule.Register(registrar, false);
            }
            RegisterChatNotificationsConfiguration(registrar).Build();
            RegisterChatsNotificationBuilder(registrar).Build();
            RegisterChatParticipantsNotificationBuilder(registrar).Build();
            RegisterChatMessagesNotificationBuilder(registrar).Build();
            RegisterChatNotifier(registrar).Build();
            RegisterChatParticipantNotifier(registrar).Build();
            RegisterChatMessageNotifier(registrar).Build();
            RegisterNotificationRouteService(registrar).Build();
            RegisterNotificationSender(registrar).Build();
        }

        public abstract IDependencyRegistrationBuilder<IChatNotificationsConfiguration> RegisterChatNotificationsConfiguration(IDependencyRegistrar registrar);
       
        public abstract IDependencyRegistrationBuilder<IChatsNotificationBuilder> RegisterChatsNotificationBuilder(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatParticipantsNotificationBuilder> RegisterChatParticipantsNotificationBuilder(IDependencyRegistrar registrar);
       
        public abstract IDependencyRegistrationBuilder<IChatMessagesNotificationBuilder> RegisterChatMessagesNotificationBuilder(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatNotifier> RegisterChatNotifier(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatParticipantNotifier> RegisterChatParticipantNotifier(IDependencyRegistrar registrar);
       
        public abstract IDependencyRegistrationBuilder<IChatMessageNotifier> RegisterChatMessageNotifier(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<INotificationRouteService> RegisterNotificationRouteService(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<INotificationSender> RegisterNotificationSender(IDependencyRegistrar registrar);

        public virtual IDependencyRegistrationBuilder<IChatServicesConfiguration> RegisterDotChatConfiguration(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterDotChatConfiguration(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatCommandSender> RegisterChatCommandSender(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatCommandSender(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatEventPublisher> RegisterChatEventPublisher(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatEventPublisher(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatsCommandBuilder> RegisterChatsCommandBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatsCommandBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsCommandBuilder> RegisterChatParticipantsCommandBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatParticipantsCommandBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesCommandBuilder> RegisterChatMessagesCommandBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatMessagesCommandBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatsEventBuilder> RegisterChatsEventBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatsEventBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsEventBuilder> RegisterChatParticipantsEventBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatParticipantsEventBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesEventBuilder> RegisterChatMessagesEventBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatMessagesEventBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IReadChatStore> RegisterReadChatStore(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterReadChatStore(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IReadChatMessageStore> RegisterReadChatMessageStore(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterReadChatMessageStore(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IReadChatParticipantStore> RegisterReadChatParticipantStore(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterReadChatParticipantStore(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IReadUserStore> RegisterReadUserStore(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterReadUserStore(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatsService> RegisterChatsService(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatsService(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsService> RegisterChatParticipantsService(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatParticipantsService(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesService> RegisterChatMessagesService(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatMessagesService(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IDotChat> RegisterDotChat(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterDotChat(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatsPermissionValidator> RegisterChatsPermissionValidator(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatsPermissionValidator(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsPermissionValidator> RegisterChatParticipantsPermissionValidator(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatParticipantsPermissionValidator(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesPermissionValidator> RegisterChatMessagesPermissionValidator(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatMessagesPermissionValidator(registrar);
        }
    }
}
