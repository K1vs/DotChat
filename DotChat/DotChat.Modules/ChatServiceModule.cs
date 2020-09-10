namespace K1vs.DotChat.Modules
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

        public abstract IDependencyRegistrationBuilder<IChatServicesConfiguration> RegisterDotChatConfiguration(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatCommandSender> RegisterChatCommandSender(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatEventPublisher> RegisterChatEventPublisher(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatsCommandBuilder> RegisterChatsCommandBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatParticipantsCommandBuilder> RegisterChatParticipantsCommandBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatMessagesCommandBuilder> RegisterChatMessagesCommandBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatsEventBuilder> RegisterChatsEventBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatParticipantsEventBuilder> RegisterChatParticipantsEventBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatMessagesEventBuilder> RegisterChatMessagesEventBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadChatStore> RegisterReadChatStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadChatMessageStore> RegisterReadChatMessageStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadChatParticipantStore> RegisterReadChatParticipantStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadUserStore> RegisterReadUserStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatsService> RegisterChatsService(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatParticipantsService> RegisterChatParticipantsService(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatMessagesService> RegisterChatMessagesService(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IDotChat> RegisterDotChat(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatsPermissionValidator> RegisterChatsPermissionValidator(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatParticipantsPermissionValidator> RegisterChatParticipantsPermissionValidator(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatMessagesPermissionValidator> RegisterChatMessagesPermissionValidator(IDependencyRegistrar registrar);
    }
}
