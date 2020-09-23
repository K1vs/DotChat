namespace K1vs.DotChat.Tests.Integration.Modules
{
    using Demo.Bus.InMemory;
    using Demo.Others;
    using Demo.Stores.InMemory;
    using Dependency;
    using K1vs.DotChat.Common.Configuration;
    using K1vs.DotChat.Common.Modules;
    using Notifiers;

    public class TestChatNotificationModule: ChatNotificationModule
    {
        private readonly EventNotificationSender _eventNotificationSender;

        public TestChatNotificationModule(InMemoryBus bus, InMemoryStore store, EventNotificationSender eventNotificationSender) : base(new TestChatServiceModule(bus, store), new ChatNotificationsConfiguration())
        {
            _eventNotificationSender = eventNotificationSender;
        }

        public override IDependencyRegistrationBuilder<INotificationSender> RegisterNotificationSender(IDependencyRegistrar registrar)
        {
            return registrar.Register(_eventNotificationSender)
                .AsSelf()
                .As<INotificationSender>()
                .AsSingleton();
        }
    }
}
