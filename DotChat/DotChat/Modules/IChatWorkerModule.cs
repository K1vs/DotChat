namespace K1vs.DotChat.Modules
{
    using System.Collections.Generic;
    using SystemMessages;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using Dependency;
    using Generators;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Stores.Chats;
    using Stores.Messages;
    using Stores.Participants;
    using Workers;

    public interface IChatWorkerModule: IChatServiceModule
    {
        IDependencyRegistrationBuilder<IChatWorkersConfiguration> RegisterChatWorkersConfiguration(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessageTimestampGenerator> RegisterChatMessageTimestampGenerator(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessageIndexGenerator> RegisterChatMessageIndexGenerator(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<ISystemMessagesBuilder> RegisterSystemMessagesBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatStore> RegisterChatStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantStore> RegisterChatParticipantStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessageStore> RegisterChatMessageStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatsWorker> RegisterChatsWorker(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsWorker> RegisterChatParticipantsWorker(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesWorker> RegisterChatMessagesWorker(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessageIndexationWorker> RegisterChatMessageIndexationWorker(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatSystemMessagesWorker> RegisterChatSystemMessagesWorker(IDependencyRegistrar registrar);
    }
}
