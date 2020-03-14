namespace K1vs.DotChat.Basic.Modules
{
    using System.Collections.Generic;
    using SystemMessages;
    using Bus;
    using Chats;
    using Common.Exceptions;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using Dependency;
    using DotChat.Chats;
    using DotChat.CommandBuilders;
    using DotChat.Configuration;
    using DotChat.EventBuilders;
    using DotChat.Generators;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Modules;
    using DotChat.Participants;
    using DotChat.Services;
    using DotChat.SystemMessages;
    using DotChat.Workers;
    using Generators;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Participants;
    using Security;
    using Stores.Chats;
    using Stores.Messages;
    using Stores.Participants;
    using Stores.Users;
    using Workers;

    public abstract class ChatWorkerModule: ChatWorkerModule<ChatServicesConfiguration, ChatWorkersConfiguration, PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, List<ChatParticipant>, ChatParticipant, ChatUser,
        List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        private readonly ChatWorkersConfiguration _chatWorkersConfiguration;

        protected ChatWorkerModule(ChatServiceModule chatServiceModule, ChatWorkersConfiguration chatWorkersConfiguration)
            : base(chatServiceModule)
        {
            _chatWorkersConfiguration = chatWorkersConfiguration;
        }

        public override void Register(IDependencyRegistrar registrar, bool rootModule)
        {
            base.Register(registrar, rootModule);
        }


        public override IDependencyRegistrationBuilder<ChatWorkersConfiguration> RegisterChatWorkersConfiguration(IDependencyRegistrar registrar)
        {
            return registrar.Register(_chatWorkersConfiguration).AsSelf().As<IChatWorkersConfiguration>().AsSingleton();
        }

        public override IDependencyRegistrationBuilder<IChatMessageTimestampGenerator> RegisterChatMessageTimestampGenerator(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessageTimestampGenerator>().AsSelf().As<IChatMessageTimestampGenerator>().AsSingleton();
        }

        public override IDependencyRegistrationBuilder<ISystemMessagesBuilder<Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterSystemMessagesBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<SystemMessagesBuilder>().AsSelf().As<ISystemMessagesBuilder<Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatsWorker<ChatInfo, List<ParticipationCandidate>, ParticipationCandidate, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatsWorker(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatsWorker>().AsSelf().As<IChatsWorker<ChatInfo, List<ParticipationCandidate>, ParticipationCandidate, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatParticipantsWorker<List<ParticipationCandidate>, ParticipationCandidate>> RegisterChatParticipantsWorker(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsWorker>().AsSelf().As<IChatParticipantsWorker<List<ParticipationCandidate>, ParticipationCandidate>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatMessagesWorker<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatMessagesWorker(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessagesWorker>().AsSelf().As<IChatMessagesWorker<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatMessageIndexationWorker<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatMessageIndexationWorker(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessageIndexationWorker>().AsSelf().As<IChatMessageIndexationWorker<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatSystemMessagesWorker<Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatSystemMessagesWorker(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatSystemMessagesWorker>().AsSelf().As<IChatSystemMessagesWorker<Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>().AsTransient();
        }
    }
}
