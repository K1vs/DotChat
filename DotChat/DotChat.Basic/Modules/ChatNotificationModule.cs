namespace K1vs.DotChat.Basic.Modules
{
    using System.Collections.Generic;
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
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Modules;
    using DotChat.NotificationBuilders;
    using DotChat.Notifiers;
    using DotChat.Participants;
    using DotChat.Services;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using NotificationBuilders;
    using Notifiers;
    using Participants;
    using Security;
    using Stores.Chats;
    using Stores.Messages;
    using Stores.Participants;
    using Stores.Users;

    public abstract class ChatNotificationModule: ChatNotificationModule<ChatServicesConfiguration, ChatNotificationsConfiguration, PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, List<ChatParticipant>, ChatParticipant, ChatUser,
        List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        private readonly ChatNotificationsConfiguration _chatNotificationsConfiguration;

        protected ChatNotificationModule(ChatServiceModule chatServiceModule, ChatNotificationsConfiguration chatNotificationsConfiguration)
            : base(chatServiceModule)
        {
            _chatNotificationsConfiguration = chatNotificationsConfiguration;
        }

        public override void Register(IDependencyRegistrar registrar, bool rootModule)
        {
            base.Register(registrar, rootModule);
        }

        public override IDependencyRegistrationBuilder<ChatNotificationsConfiguration> RegisterChatNotificationsConfiguration(IDependencyRegistrar registrar)
        {
            return registrar.Register(_chatNotificationsConfiguration).AsSelf().As<IChatNotificationsConfiguration>().AsSingleton();
        }

        public override IDependencyRegistrationBuilder<IChatsNotificationBuilder<PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatsNotificationBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatsNotificationBuilder>().AsSelf().As<IChatsNotificationBuilder<PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatParticipantsNotificationBuilder<List<ParticipationResult>, ParticipationResult, ChatParticipant>> RegisterChatParticipantsNotificationBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantsNotificationBuilder>().AsSelf().As<IChatParticipantsNotificationBuilder<List<ParticipationResult>, ParticipationResult, ChatParticipant>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatMessagesNotificationBuilder<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatMessagesNotificationBuilder(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessagesNotificationBuilder>().AsSelf().As<IChatMessagesNotificationBuilder<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatNotifier<Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatNotifier(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatNotifier>().AsSelf().As<IChatNotifier<Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatParticipantNotifier<List<ParticipationResult>, ParticipationResult, ChatParticipant>> RegisterChatParticipantNotifier(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatParticipantNotifier>().AsSelf().As<IChatParticipantNotifier<List<ParticipationResult>, ParticipationResult, ChatParticipant>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<IChatMessageNotifier<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>> RegisterChatMessageNotifier(IDependencyRegistrar registrar)
        {
            return registrar.Register<ChatMessageNotifier>().AsSelf().As<IChatMessageNotifier<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>().AsTransient();
        }

        public override IDependencyRegistrationBuilder<INotificationRouteService> RegisterNotificationRouteService(IDependencyRegistrar registrar)
        {
            return registrar.Register<NotificationRouteService>().AsSelf().As<INotificationRouteService>().AsSingleton();
        }
    }
}
