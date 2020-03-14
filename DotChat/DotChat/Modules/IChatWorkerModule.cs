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

    public interface IChatWorkerModule<out TDotChatConfiguration, out TChatWorkersConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser,
        TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, in TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, in TPagingOptions>:
        IChatServiceModule<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser,
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
        IDependencyRegistrationBuilder<TChatWorkersConfiguration> RegisterChatWorkersConfiguration(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessageTimestampGenerator> RegisterChatMessageTimestampGenerator(IDependencyRegistrar registrar);
        IDependencyRegistrationBuilder<IChatMessageIndexGenerator> RegisterChatMessageIndexGenerator(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<ISystemMessagesBuilder<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>>
            RegisterSystemMessagesBuilder(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IChatStore<TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TPagingOptions>>
            RegisterChatStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantStore<TChatParticipant, TChatUser>> RegisterChatParticipantStore(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TChatMessagesPagedResult, TPagingOptions>>
            RegisterChatMessageStore(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IChatsWorker<TChatInfo, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatsWorker(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsWorker<TParticipationCandidateCollection, TParticipationCandidate>> RegisterChatParticipantsWorker(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesWorker<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> 
            RegisterChatMessagesWorker(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessageIndexationWorker<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>>
            RegisterChatMessageIndexationWorker(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatSystemMessagesWorker<TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatSystemMessagesWorker(IDependencyRegistrar registrar);
    }
}
