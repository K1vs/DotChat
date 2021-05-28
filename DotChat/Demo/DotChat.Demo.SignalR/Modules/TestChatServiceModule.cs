namespace K1vs.DotChat.Demo.SignalR.Modules
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
    using K1vs.DotChat.Bus;
    using K1vs.DotChat.Stores.Chats;
    using K1vs.DotChat.Stores.Messages;
    using K1vs.DotChat.Stores.Participants;
    using K1vs.DotChat.Stores.Users;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class TestChatServiceModule: ChatServiceModule
    {
        private readonly InMemoryBus _bus;
        private readonly InMemoryStore _store;

        public TestChatServiceModule(InMemoryBus bus, InMemoryStore store) : base(new ChatServicesConfiguration())
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

        public override IDependencyRegistrationBuilder<IReadChatStore<ChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>> RegisterReadChatStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryReadChatStore>()
                .AsSelf()
                .As<IReadChatStore<ChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IReadChatMessageStore<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>> RegisterReadChatMessageStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryReadChatMessageStore>()
                .AsSelf()
                .As<IReadChatMessageStore<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IReadChatParticipantStore<ChatParticipant>> RegisterReadChatParticipantStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryReadChatParticipantStore>()
                .AsSelf()
                .As<IReadChatParticipantStore<ChatParticipant>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IReadUserStore<ChatUser>> RegisterReadUserStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryReadUserStore>()
                .AsSelf()
                .As<IReadUserStore<ChatUser>>()
                .AsTransient();
        }
    }
}
