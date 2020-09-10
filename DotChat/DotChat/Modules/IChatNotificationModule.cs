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

    public interface IChatNotificationModule: IChatServiceModule
    {
        IDependencyRegistrationBuilder<IChatNotificationsConfiguration> RegisterChatNotificationsConfiguration(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatsNotificationBuilder> RegisterChatsNotificationBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantsNotificationBuilder> RegisterChatParticipantsNotificationBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessagesNotificationBuilder> RegisterChatMessagesNotificationBuilder(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatNotifier> RegisterChatNotifier(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatParticipantNotifier> RegisterChatParticipantNotifier(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<IChatMessageNotifier> RegisterChatMessageNotifier(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<INotificationRouteService> RegisterNotificationRouteService(IDependencyRegistrar registrar);

        IDependencyRegistrationBuilder<INotificationSender> RegisterNotificationSender(IDependencyRegistrar registrar);
    }
}
