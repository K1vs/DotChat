namespace K1vs.DotChat.Demo.SignalR.Modules
{
    using Basic.Configuration;
    using Basic.Modules;
    using Dependency;
    using K1vs.DotChat.Basic.Chats;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Basic.Participants;
    using K1vs.DotChat.Implementations.SignalR;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.Models.Messages.Typed;
    using K1vs.DotChat.Models.Participants;
    using Notifiers;
    using System.Collections.Generic;
    using Clients;
    using Hubs;

    public class TestChatNotificationModule: ChatNotificationModule
    {
        public TestChatNotificationModule() : base(null, new ChatNotificationsConfiguration())
        {
        }

        public override IDependencyRegistrationBuilder<INotificationSender> RegisterNotificationSender(IDependencyRegistrar registrar)
        {
            return registrar.Register<SignalRNotificationSender<ChatsHub, IChatsClient, ChatParticipantsHub, IChatParticipantsClient, ChatMessagesHub, IChatMessagesClient, PersonalizedChat, Chat,
                ChatInfo, List<ParticipationModificationResult>, ParticipationModificationResult, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessage, ChatMessageInfo, TextMessage,
                QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>()
                .AsSelf()
                .As<INotificationSender>()
                .AsSingleton();
        }
    }
}
