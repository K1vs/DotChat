namespace K1vs.DotChat.Modules
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

    public abstract class ChatWorkerModule<TDotChatConfiguration, TChatWorkersConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser,
            TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions> :
        IChatWorkerModule<TDotChatConfiguration, TChatWorkersConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser,
            TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions>
        where TDotChatConfiguration : IChatServicesConfiguration
        where TChatWorkersConfiguration : IChatWorkersConfiguration
        where TPersonalizedChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection : IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo : IChatInfo
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidates : IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatUser : IChatUser
        where TChatMessageCollection : IReadOnlyCollection<TChatMessage>
        where TChatMessage : IChatMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TChatsPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TChatMessagesPagedResult : IPagedResult<TChatMessageCollection, TChatMessage>
        where TPagingOptions : IPagingOptions
    {
        private readonly ChatServiceModule<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection,
            TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult,
            TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate,
            TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageCollection, TChatMessage,
            TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment,
            TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter,
            TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions> _chatServiceModule;

        protected ChatWorkerModule(ChatServiceModule<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions> chatServiceModule)
        {
            _chatServiceModule = chatServiceModule;
        }

        public virtual void Register(IDependencyRegistrar registrar, bool rootModule)
        {
            _chatServiceModule.Register(registrar, false);
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

        public abstract IDependencyRegistrationBuilder<TChatWorkersConfiguration> RegisterChatWorkersConfiguration(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatMessageTimestampGenerator> RegisterChatMessageTimestampGenerator(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatMessageIndexGenerator> RegisterChatMessageIndexGenerator(IDependencyRegistrar registrar);
       
        public abstract IDependencyRegistrationBuilder<ISystemMessagesBuilder<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> 
            RegisterSystemMessagesBuilder(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> 
            RegisterChatStore(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatParticipantStore<TChatParticipant, TChatUser>> RegisterChatParticipantStore(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> 
            RegisterChatMessageStore(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatsWorker<TChatInfo, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatsWorker(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatParticipantsWorker<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsWorker(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatMessagesWorker<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> 
            RegisterChatMessagesWorker(IDependencyRegistrar registrar);
       
        public abstract IDependencyRegistrationBuilder<IChatMessageIndexationWorker<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> 
            RegisterChatMessageIndexationWorker(IDependencyRegistrar registrar);
        
        public abstract IDependencyRegistrationBuilder<IChatSystemMessagesWorker<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant>> RegisterChatSystemMessagesWorker(IDependencyRegistrar registrar);
        
        public IDependencyRegistrationBuilder<TDotChatConfiguration> RegisterDotChatConfiguration(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterDotChatConfiguration(registrar);
        }

        public IDependencyRegistrationBuilder<IChatCommandSender> RegisterChatCommandSender(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatCommandSender(registrar);
        }

        public IDependencyRegistrationBuilder<IChatEventPublisher> RegisterChatEventPublisher(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatEventPublisher(registrar);
        }

        public IDependencyRegistrationBuilder<IChatsCommandBuilder<TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatsCommandBuilder(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatsCommandBuilder(registrar);
        }

        public IDependencyRegistrationBuilder<IChatParticipantsCommandBuilder<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsCommandBuilder(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatParticipantsCommandBuilder(registrar);
        }

        public IDependencyRegistrationBuilder<IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatMessagesCommandBuilder(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatMessagesCommandBuilder(registrar);
        }

        public IDependencyRegistrationBuilder<IChatsEventBuilder<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant>> RegisterChatsEventBuilder(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatsEventBuilder(registrar);
        }

        public IDependencyRegistrationBuilder<IChatParticipantsEventBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant>> RegisterChatParticipantsEventBuilder(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatParticipantsEventBuilder(registrar);
        }

        public IDependencyRegistrationBuilder<IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatMessagesEventBuilder(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatMessagesEventBuilder(registrar);
        }

        public IDependencyRegistrationBuilder<IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> RegisterReadChatStore(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterReadChatStore(registrar);
        }

        public IDependencyRegistrationBuilder<IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> RegisterReadChatMessageStore(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterReadChatMessageStore(registrar);
        }

        public IDependencyRegistrationBuilder<IReadChatParticipantStore<TChatParticipant>> RegisterReadChatParticipantStore(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterReadChatParticipantStore(registrar);
        }

        public IDependencyRegistrationBuilder<IReadUserStore<TChatUser>> RegisterReadUserStore(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterReadUserStore(registrar);
        }

        public IDependencyRegistrationBuilder<IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> RegisterChatsService(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatsService(registrar);
        }

        public IDependencyRegistrationBuilder<IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsService(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatParticipantsService(registrar);
        }

        public IDependencyRegistrationBuilder<IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> RegisterChatMessagesService(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatMessagesService(registrar);
        }

        public IDependencyRegistrationBuilder<IDotChat<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions>> RegisterDotChat(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterDotChat(registrar);
        }

        public IDependencyRegistrationBuilder<IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> RegisterChatsPermissionValidator(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatsPermissionValidator(registrar);
        }

        public IDependencyRegistrationBuilder<IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsPermissionValidator(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatParticipantsPermissionValidator(registrar);
        }

        public IDependencyRegistrationBuilder<IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> RegisterChatMessagesPermissionValidator(IDependencyRegistrar registrar)
        {
            return _chatServiceModule.RegisterChatMessagesPermissionValidator(registrar);
        }
    }
}
