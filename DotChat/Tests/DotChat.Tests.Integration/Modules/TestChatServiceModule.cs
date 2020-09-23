namespace K1vs.DotChat.Tests.Integration.Modules
{
    using System.Collections.Generic;
    using Basic.Chats;
    using Basic.Configuration;
    using Basic.Messages;
    using Basic.Messages.Typed;
    using Basic.Modules;
    using Bus;
    using Common.Filters;
    using Common.Paging;
    using Demo.Bus.InMemory;
    using Demo.Stores.InMemory;
    using Demo.Stores.InMemory.Chats;
    using Demo.Stores.InMemory.Messages;
    using Demo.Stores.InMemory.Participants;
    using Demo.Stores.InMemory.Users;
    using Dependency;
    using K1vs.DotChat.Common.Configuration;
    using K1vs.DotChat.Common.Modules;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Stores.Chats;
    using Stores.Messages;
    using Stores.Participants;
    using Stores.Users;

    public class TestChatServiceModule: ChatServiceModule
    {
        private readonly InMemoryBus _bus;
        private readonly InMemoryStore _store;

        public TestChatServiceModule(InMemoryBus bus, InMemoryStore store)
        {
            _bus = bus;
            _store = store;
        }

        public override void Register(IDependencyRegistrar registrar, bool rootModule)
        {
            registrar.Register(_bus)
                .AsSelf()
                .AsSingleton()
                .Build();
            registrar.Register(_store)
                .AsSelf()
                .AsSingleton()
                .Build();
            base.Register(registrar, rootModule);
        }

        public override IDependencyRegistrationBuilder<IChatCommandSender> RegisterChatCommandSender(IDependencyRegistrar registrar)
        {
            return registrar.Register<IChatCommandSender>(r => r.Resolve<InMemoryBus>())
                .AsSelf()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatEventPublisher> RegisterChatEventPublisher(IDependencyRegistrar registrar)
        {
            return registrar.Register<IChatEventPublisher>(r => r.Resolve<InMemoryBus>())
                .AsSelf()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IReadChatStore> RegisterReadChatStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryReadChatStore>()
                .AsSelf()
                .As<IReadChatStore>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IReadChatMessageStore> RegisterReadChatMessageStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryReadChatMessageStore>()
                .AsSelf()
                .As<IReadChatMessageStore>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IReadChatParticipantStore> RegisterReadChatParticipantStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryReadChatParticipantStore>()
                .AsSelf()
                .As<IReadChatParticipantStore>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IReadUserStore> RegisterReadUserStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryReadUserStore>()
                .AsSelf()
                .As<IReadUserStore>()
                .AsTransient();
        }
    }
}
