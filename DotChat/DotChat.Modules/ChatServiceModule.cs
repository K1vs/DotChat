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

    public abstract class ChatServiceModule<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser,
        TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions>: 
        IChatServiceModule<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser,
        TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions>
        where TDotChatConfiguration: IChatServicesConfiguration
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

        public abstract IDependencyRegistrationBuilder<TDotChatConfiguration> RegisterDotChatConfiguration(
            IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatCommandSender> RegisterChatCommandSender(
            IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatEventPublisher> RegisterChatEventPublisher(
            IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatsCommandBuilder<TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatsCommandBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatParticipantsCommandBuilder<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsCommandBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> 
            RegisterChatMessagesCommandBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatsEventBuilder<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatsEventBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatParticipantsEventBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant>> RegisterChatParticipantsEventBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> 
            RegisterChatMessagesEventBuilder(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> 
            RegisterReadChatStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> 
            RegisterReadChatMessageStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadChatParticipantStore<TChatParticipant>> RegisterReadChatParticipantStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IReadUserStore<TChatUser>> RegisterReadUserStore(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> 
            RegisterChatsService(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsService(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> 
            RegisterChatMessagesService(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IDotChat<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions>> 
            RegisterDotChat(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> 
            RegisterChatsPermissionValidator(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsPermissionValidator(IDependencyRegistrar registrar);

        public abstract IDependencyRegistrationBuilder<IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>> 
            RegisterChatMessagesPermissionValidator(IDependencyRegistrar registrar);
    }
}
