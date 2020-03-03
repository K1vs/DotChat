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

    public interface IChatServiceModule<out TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, out TParticipationResultCollection, TParticipationResult, in TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser,
        TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, in TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, in TPagingOptions>: IDependencyModule
        where TDotChatConfiguration: IChatServicesConfiguration
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
        IDependencyRegistrationBuilder<TDotChatConfiguration> RegisterDotChatConfiguration(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IChatCommandSender> RegisterChatCommandSender(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatEventPublisher> RegisterChatEventPublisher(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IChatsCommandBuilder<TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatsCommandBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsCommandBuilder<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsCommandBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesCommandBuilder<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> 
            RegisterChatMessagesCommandBuilder(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IChatsEventBuilder<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant>> RegisterChatsEventBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsEventBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant>> RegisterChatParticipantsEventBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesEventBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection,  TContactMessage>> 
            RegisterChatMessagesEventBuilder(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IReadChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> 
            RegisterReadChatStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>>
            RegisterReadChatMessageStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IReadChatParticipantStore<TChatParticipant>> RegisterReadChatParticipantStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IReadUserStore<TChatUser>> RegisterReadUserStore(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IChatsService<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>> 
            RegisterChatsService(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate>> 
            RegisterChatParticipantsService(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesService<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>>
            RegisterChatMessagesService(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IDotChat<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions>>
            RegisterDotChat(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>>
            RegisterChatsPermissionValidator(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate>>
            RegisterChatParticipantsPermissionValidator(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>>
            RegisterChatMessagesPermissionValidator(IDependencyRegistrar registrar);
    }
}
