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
    using K1vs.DotChat.Security;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Services;
    using Stores.Chats;
    using Stores.Messages;
    using Stores.Participants;
    using Stores.Users;

    public interface IChatServiceModule: IDependencyModule
    {
        IDependencyRegistrationBuilder<IChatServicesConfiguration> RegisterDotChatConfiguration(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatCommandSender> RegisterChatCommandSender(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatEventPublisher> RegisterChatEventPublisher(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatsCommandBuilder> RegisterChatsCommandBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsCommandBuilder> RegisterChatParticipantsCommandBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesCommandBuilder> RegisterChatMessagesCommandBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatsEventBuilder> RegisterChatsEventBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsEventBuilder> RegisterChatParticipantsEventBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesEventBuilder> RegisterChatMessagesEventBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IReadChatStore> RegisterReadChatStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IReadChatMessageStore> RegisterReadChatMessageStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IReadChatParticipantStore> RegisterReadChatParticipantStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IReadUserStore> RegisterReadUserStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatsService> RegisterChatsService(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsService> RegisterChatParticipantsService(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesService> RegisterChatMessagesService(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IDotChat> RegisterDotChat(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatsPermissionValidator> RegisterChatsPermissionValidator(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsPermissionValidator> RegisterChatParticipantsPermissionValidator(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesPermissionValidator> RegisterChatMessagesPermissionValidator(IDependencyRegistrar registrar);
    }
}
