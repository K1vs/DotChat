namespace K1vs.DotChat.Modules
{
    using System.Collections.Generic;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using Dependency;
    using Messages;
    using Messages.Typed;
    using NotificationBuilders;
    using Notifiers;
    using Participants;

    public interface IChatNotificationModule<out TDotChatConfiguration, TChatNotificationsConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser,
        TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, in TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, in TPagingOptions>:
        IChatServiceModule<TDotChatConfiguration, TPersonalizedChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TParticipationResultCollection, TParticipationResult, TParticipationCandidates, TParticipationCandidateCollection, TParticipationCandidate, TChatParticipantCollection, TChatParticipant, TChatUser,
        TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TChatsPagedResult, TChatMessagesPagedResult, TPagingOptions>
        where TDotChatConfiguration : IChatServicesConfiguration
        where TChatNotificationsConfiguration : IChatNotificationsConfiguration
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
        IDependencyRegistrationBuilder<TChatNotificationsConfiguration> RegisterChatNotificationsConfiguration(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IChatsNotificationBuilder<TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatsNotificationBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsNotificationBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant>> RegisterChatParticipantsNotificationBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesNotificationBuilder<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>>
            RegisterChatMessagesNotificationBuilder(IDependencyRegistrar registrar);


        IDependencyRegistrationBuilder<IChatNotifier<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>> RegisterChatNotifier(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantNotifier<TParticipationResultCollection, TParticipationResult, TChatParticipant>> RegisterChatParticipantNotifier(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessageNotifier<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>>
            RegisterChatMessageNotifier(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<INotificationRouteService> RegisterNotificationRouteService(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<INotificationSender> RegisterNotificationSender(IDependencyRegistrar registrar);
    }
}
