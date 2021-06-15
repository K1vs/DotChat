namespace K1vs.DotChat.Demo.AspNetCore.SignalR.Modules
{
    using System.Collections.Generic;
    using Basic.Chats;
    using Basic.Configuration;
    using Basic.Messages;
    using Basic.Messages.Typed;
    using Basic.Modules;
    using Common.Filters;
    using Common.Paging;
    using Demo.Bus.InMemory;
    using Demo.Others;
    using Demo.Stores.InMemory;
    using Demo.Stores.InMemory.Chats;
    using Demo.Stores.InMemory.Messages;
    using Demo.Stores.InMemory.Participants;
    using Dependency;
    using Generators;
    using K1vs.DotChat.Stores.Chats;
    using K1vs.DotChat.Stores.Messages;
    using K1vs.DotChat.Stores.Participants;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class TestChatWorkerModule: ChatWorkerModule
    {
        public TestChatWorkerModule() : base(new TestChatServiceModule(), new ChatWorkersConfiguration() { DisableSystemMessages = true })
        {
        }

        public override IDependencyRegistrationBuilder<IChatMessageIndexGenerator> RegisterChatMessageIndexGenerator(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryChatMessageIndexGenerator>()
                .AsSelf()
                .As<IChatMessageIndexGenerator>()
                .AsSingleton();
        }

        public override IDependencyRegistrationBuilder<IChatStore<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, List<ParticipationCandidate>, ParticipationCandidate, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>> RegisterChatStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryChatStore>()
                .AsSelf()
                .As<IChatStore<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, List<ParticipationCandidate>, ParticipationCandidate, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatParticipantStore<ChatParticipant, ChatUser>> RegisterChatParticipantStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryChatParticipantStore>()
                .AsSelf()
                .As<IChatParticipantStore<ChatParticipant, ChatUser>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatMessageStore<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>> RegisterChatMessageStore(IDependencyRegistrar registrar)
        {
            return registrar.Register<InMemoryChatMessageStore>()
                .AsSelf()
                .As<IChatMessageStore<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage,
                    QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage,
                    List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>,
                    PagingOptions>>()
                .AsTransient();
        }
    }
}
