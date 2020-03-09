namespace K1vs.DotChat.Demo.SignalR.Modules
{
    using Basic.Configuration;
    using Basic.Modules;
    using Demo.Bus.InMemory;
    using Demo.Others;
    using Demo.Stores.InMemory;
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

    public class TestChatNotificationModule: ChatNotificationModule
    {
        public TestChatNotificationModule() : base(null, new ChatNotificationsConfiguration())
        {
        }

        public override IDependencyRegistrationBuilder<INotificationSender> RegisterNotificationSender(IDependencyRegistrar registrar)
        {
            return registrar.Register<SignalRNotificationSender<ChatsHub, IChatsClient<PersonalizedChat, ChatInfo, List<ChatParticipant>, ChatParticipant>, ChatParticipantsHub, IChatParticipantsClient, ChatMessagesHub, IChatMessagesClient, PersonalizedChat,
                ChatInfo, List<ParticipationResult>, ParticipationResult, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessage, ChatMessageInfo, TextMessage,
                QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>>()
                .AsSelf()
                .As<INotificationSender>()
                .AsSingleton();
        }
    }
}
