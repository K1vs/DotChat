namespace K1vs.DotChat.Common.Modules
{
    using System.Collections.Generic;
    using SystemMessages;
    using Bus;
    using Chats;
    using CommandBuilders;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using Dependency;
    using EventBuilders;
    using Generators;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Security;
    using Services;
    using Stores.Chats;
    using Stores.Messages;
    using Stores.Participants;
    using Stores.Users;
    using Workers;
    using K1vs.DotChat.Modules;
    using K1vs.DotChat.Configuration;
    using K1vs.DotChat.CommandBuilders;

    public abstract class ChatWorkerModule: IChatWorkerModule
    {
        protected readonly ChatServiceModule ChatServiceModule;

        protected ChatWorkerModule(ChatServiceModule chatServiceModule)
        {
            ChatServiceModule = chatServiceModule;
        }

        public virtual void Register(IDependencyRegistrar registrar, bool rootModule)
        {
            if(ChatServiceModule != null)
            {
                ChatServiceModule.Register(registrar, false);
            }
            RegisterChatWorkersConfiguration(registrar).Build();
            RegisterChatMessageTimestampGenerator(registrar).Build();
            RegisterChatMessageIndexGenerator(registrar).Build();
            RegisterSystemMessagesBuilder(registrar).Build();
            RegisterChatStore(registrar).Build();
            RegisterChatParticipantStore(registrar).Build();
            RegisterChatMessageStore(registrar).Build();
            RegisterChatsWorker(registrar).Build();
            RegisterChatParticipantsWorker(registrar).Build();
            RegisterChatMessagesWorker(registrar).Build();
            RegisterChatMessageIndexationWorker(registrar).Build();
            RegisterChatSystemMessagesWorker(registrar).Build();
        }

        public abstract IDependencyRegistrationBuilder<IChatWorkersConfiguration> RegisterChatWorkersConfiguration(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatMessageTimestampGenerator> RegisterChatMessageTimestampGenerator(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatMessageIndexGenerator> RegisterChatMessageIndexGenerator(IDependencyRegistrar registrar);
       
        public abstract IDependencyRegistrationBuilder<ISystemMessagesBuilder> RegisterSystemMessagesBuilder(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatStore> RegisterChatStore(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatParticipantStore> RegisterChatParticipantStore(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatMessageStore> RegisterChatMessageStore(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatsWorker> RegisterChatsWorker(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatParticipantsWorker> RegisterChatParticipantsWorker(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatMessagesWorker> RegisterChatMessagesWorker(IDependencyRegistrar registrar);
       
        public abstract IDependencyRegistrationBuilder<IChatMessageIndexationWorker> RegisterChatMessageIndexationWorker(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatSystemMessagesWorker> RegisterChatSystemMessagesWorker(IDependencyRegistrar registrar);

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
