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
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
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
        protected readonly ChatServiceModule<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection,
            TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult,
            TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate,
            TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageCollection, TChatMessage,
            TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment,
            TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter,
            TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions> ChatServiceModule;

        protected ChatWorkerModule(ChatServiceModule<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions> chatServiceModule)
        {
            ChatServiceModule = chatServiceModule;
        }

        public virtual void Register(IDependencyRegistrar registrar, bool rootModule)
        {
            if(ChatServiceModule != null)
            {
                ChatServiceModule.Register(registrar, false);
            }
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
        
        public abstract IDependencyRegistrationBuilder<IChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> 
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
        
        public abstract IDependencyRegistrationBuilder<IChatSystemMessagesWorker<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatSystemMessagesWorker(IDependencyRegistrar registrar);

        public virtual IDependencyRegistrationBuilder<TDotChatConfiguration> RegisterDotChatConfiguration(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterDotChatConfiguration(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatCommandSender> RegisterChatCommandSender(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatCommandSender(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatEventPublisher> RegisterChatEventPublisher(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatEventPublisher(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatsCommandBuilder<TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatsCommandBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatsCommandBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsCommandBuilder<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsCommandBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatParticipantsCommandBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatMessagesCommandBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatMessagesCommandBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatsEventBuilder<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatsEventBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatsEventBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsEventBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant>> RegisterChatParticipantsEventBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatParticipantsEventBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatMessagesEventBuilder(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatMessagesEventBuilder(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> RegisterReadChatStore(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterReadChatStore(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> RegisterReadChatMessageStore(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterReadChatMessageStore(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IReadChatParticipantStore<TChatParticipant>> RegisterReadChatParticipantStore(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterReadChatParticipantStore(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IReadUserStore<TChatUser>> RegisterReadUserStore(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterReadUserStore(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> RegisterChatsService(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatsService(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsService(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatParticipantsService(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> RegisterChatMessagesService(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatMessagesService(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IDotChat<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions>> RegisterDotChat(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterDotChat(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> RegisterChatsPermissionValidator(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatsPermissionValidator(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsPermissionValidator(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatParticipantsPermissionValidator(registrar);
        }

        public virtual IDependencyRegistrationBuilder<IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> RegisterChatMessagesPermissionValidator(IDependencyRegistrar registrar)
        {
            return ChatServiceModule.RegisterChatMessagesPermissionValidator(registrar);
        }
    }
}
