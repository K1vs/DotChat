namespace K1vs.DotChat.Basic.Modules
{
    using System;
    using System.Collections.Generic;
    using Bus;
    using Chats;
    using CommandBuilders;
    using Common.Exceptions;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using Dependency;
    using DotChat.Chats;
    using DotChat.CommandBuilders;
    using DotChat.Configuration;
    using DotChat.EventBuilders;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Modules;
    using DotChat.Participants;
    using DotChat.Security;
    using DotChat.Services;
    using EventBuilders;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Participants;
    using Security;
    using Services;
    using Stores.Chats;
    using Stores.Messages;
    using Stores.Participants;
    using Stores.Users;

    public abstract class ChatServiceModule: ChatServiceModule<ChatServicesConfiguration, PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, List<ChatParticipant>, ChatParticipant, ChatUser,
        List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        private readonly ChatServicesConfiguration _chatServicesConfiguration;

        protected ChatServiceModule(ChatServicesConfiguration chatServicesConfiguration)
        {
            _chatServicesConfiguration = chatServicesConfiguration;
        }

        public override void Register(IDependencyRegistrar registrar, bool rootModule)
        {
            base.Register(registrar, rootModule);
        }

        public override IDependencyRegistrationBuilder<ChatServicesConfiguration> RegisterDotChatConfiguration(IDependencyRegistrar registrar)
        {
            return registrar.Register(_chatServicesConfiguration)
                .AsSelf()
                .As<IChatServicesConfiguration>()
                .AsSingleton();
        }

        public override IDependencyRegistrationBuilder<IChatsCommandBuilder<ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate>> RegisterChatsCommandBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatsCommandBuilder>()
                .AsSelf()
                .As<IChatsCommandBuilder<ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatParticipantsCommandBuilder<List<ParticipationCandidate>, ParticipationCandidate>> RegisterChatParticipantsCommandBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsCommandBuilder>()
                .AsSelf()
                .As<IChatParticipantsCommandBuilder<List<ParticipationCandidate>, ParticipationCandidate>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatMessagesCommandBuilder<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatMessagesCommandBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessagesCommandBuilder>()
                .AsSelf()
                .As<IChatMessagesCommandBuilder<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatsEventBuilder<Chat, ChatInfo, List<ChatParticipant>, ChatParticipant>> RegisterChatsEventBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatsEventBuilder>()
                .AsSelf()
                .As<IChatsEventBuilder<Chat, ChatInfo, List<ChatParticipant>, ChatParticipant>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatParticipantsEventBuilder<List<ParticipationResult>, ParticipationResult, ChatParticipant>> RegisterChatParticipantsEventBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsEventBuilder>()
                .AsSelf()
                .As<IChatParticipantsEventBuilder<List<ParticipationResult>, ParticipationResult, ChatParticipant>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatMessagesEventBuilder<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatMessagesEventBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessagesEventBuilder>()
                .AsSelf()
                .As<IChatMessagesEventBuilder<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatsService<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>> RegisterChatsService(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatsService>()
                .AsSelf()
                .As<IChatsService<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatParticipantsService<List<ParticipationCandidate>, ParticipationCandidate>> RegisterChatParticipantsService(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsService>()
                .AsSelf()
                .As<IChatParticipantsService<List<ParticipationCandidate>, ParticipationCandidate>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatMessagesService<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>> RegisterChatMessagesService(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessagesService>()
                .AsSelf()
                .As<IChatMessagesService<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IDotChat<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, List<ChatParticipant>, ChatParticipant, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>> 
            RegisterDotChat(IDependencyRegistrar registrar)
        {
            return registrar.Register<DotChatFacade>()
                .AsSelf()
                .As<IDotChat<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, List<ChatParticipant>, ChatParticipant, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>>()
                .As<IDotChat>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatsPermissionValidator<List<PersonalizedChat>, PersonalizedChat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>> 
            RegisterChatsPermissionValidator(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatsPermissionValidator>()
                .AsSelf()
                .As<IChatsPermissionValidator<List<PersonalizedChat>, PersonalizedChat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatParticipantsPermissionValidator<List<ParticipationCandidate>, ParticipationCandidate>> 
            RegisterChatParticipantsPermissionValidator(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsPermissionValidator>()
                .AsSelf()
                .As<IChatParticipantsPermissionValidator<List<ParticipationCandidate>, ParticipationCandidate>>()
                .AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatMessagesPermissionValidator<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>> 
            RegisterChatMessagesPermissionValidator(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessagesPermissionValidator>()
                .AsSelf()
                .As<IChatMessagesPermissionValidator<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>>()
                .AsTransient();
        }
    }
}
