namespace K1vs.DotChat.Common.Modules
{
    using System;
    using System.Collections.Generic;
    using Bus;
    using Chats;
    using CommandBuilders;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using Dependency;
    using EventBuilders;
    using K1vs.DotChat.CommandBuilders;
    using K1vs.DotChat.Configuration;
    using K1vs.DotChat.Modules;
    using K1vs.DotChat.Security;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Services;
    using Stores.Chats;
    using Stores.Messages;
    using Stores.Participants;
    using Stores.Users;

    public abstract class ChatServiceModule: IChatServiceModule
    {
        public ChatServiceModule()
        {

        }

        public virtual void Register(IDependencyRegistrar registrar, bool rootModule)
        {
            RegisterDotChatConfiguration(registrar).Build();
            RegisterChatCommandSender(registrar).Build();
            RegisterChatEventPublisher(registrar).Build();
            RegisterChatsCommandBuilder(registrar).Build();
            RegisterChatParticipantsCommandBuilder(registrar).Build();
            RegisterChatMessagesCommandBuilder(registrar).Build();
            RegisterChatsEventBuilder(registrar).Build();
            RegisterChatParticipantsEventBuilder(registrar).Build();
            RegisterChatMessagesEventBuilder(registrar).Build();
            RegisterReadChatStore(registrar).Build();
            RegisterReadChatMessageStore(registrar).Build();
            RegisterReadChatParticipantStore(registrar).Build();
            RegisterReadUserStore(registrar).Build();
            RegisterChatsService(registrar).Build();
            RegisterChatParticipantsService(registrar).Build();
            RegisterChatMessagesService(registrar).Build();
            RegisterDotChat(registrar).Build();
            RegisterChatsPermissionValidator(registrar).Build();
            RegisterChatParticipantsPermissionValidator(registrar).Build();
            RegisterChatMessagesPermissionValidator(registrar).Build();
        }

        public virtual IDependencyRegistrationBuilder<IChatServicesConfiguration> RegisterDotChatConfiguration(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatServicesConfiguration>()
                .AsSelf()
                .As<IChatServicesConfiguration>();
        }

        public abstract IDependencyRegistrationBuilder<IChatCommandSender> RegisterChatCommandSender(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatEventPublisher> RegisterChatEventPublisher(IDependencyRegistrar registrar);

        public virtual IDependencyRegistrationBuilder<IChatsCommandBuilder> RegisterChatsCommandBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatsCommandBuilder>()
                .AsSelf()
                .As<IChatsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsCommandBuilder> RegisterChatParticipantsCommandBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesCommandBuilder> RegisterChatMessagesCommandBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatsEventBuilder> RegisterChatsEventBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsEventBuilder> RegisterChatParticipantsEventBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesEventBuilder> RegisterChatMessagesEventBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public abstract IDependencyRegistrationBuilder<IReadChatStore> RegisterReadChatStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadChatMessageStore> RegisterReadChatMessageStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadChatParticipantStore> RegisterReadChatParticipantStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadUserStore> RegisterReadUserStore(IDependencyRegistrar registrar);

        public virtual IDependencyRegistrationBuilder<IChatsService> RegisterChatsService(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsService> RegisterChatParticipantsService(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesService> RegisterChatMessagesService(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IDotChat> RegisterDotChat(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatsPermissionValidator> RegisterChatsPermissionValidator(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsPermissionValidator> RegisterChatParticipantsPermissionValidator(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder>();
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesPermissionValidator> RegisterChatMessagesPermissionValidator(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessagesPermissionValidator>()
                .AsSelf()
                .As<IChatMessagesPermissionValidator>();
        }
    }
}
