namespace K1vs.DotChat.Tests.Integration.Tools
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Basic;
    using Basic.Notifiers;
    using Basic.Workers;
    using Demo.Bus.InMemory;
    using Demo.Others;
    using Demo.Stores.InMemory;
    using Implementations.DependencyCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Participants;
    using Modules;

    public class TestContext
    {
        public IReadOnlyDictionary<string, ChatUser> Users = new Dictionary<string, ChatUser>
        {
            {"A", new ChatUser(Guid.NewGuid(), "A") },
            {"B", new ChatUser(Guid.NewGuid(), "B") },
            {"C", new ChatUser(Guid.NewGuid(), "C") },
            {"D", new ChatUser(Guid.NewGuid(), "D") },
            {"E", new ChatUser(Guid.NewGuid(), "E") }
        };

        public IReadOnlyDictionary<string, TestUserContext> UserContexts => _userContexts;
        public Task NotificationBusTask { get; }
        public Task WorkerBusTask { get; }

        private readonly ConcurrentDictionary<string, Guid> _chatIds = new ConcurrentDictionary<string, Guid>();
        private readonly Dictionary<string, TestUserContext> _userContexts = new Dictionary<string, TestUserContext>();
        private readonly IServiceProvider _chatNotificationModuleProvider;
        private readonly IServiceProvider _chatWorkerModuleProvider;

        public TestContext()
        {
            var queue = new InMemoryQueues();
            var store = new InMemoryStore(Users.Values);

            var eventNotificationSender = new EventNotificationSender();
            var notificationHandlers = new List<Type>()
            {
                typeof(ChatNotifier),
                typeof(ChatMessageNotifier),
                typeof(ChatParticipantNotifier)
            };
            var notificationBus = new InMemoryBus(queue, r => r.NotificationQueue, type => _chatNotificationModuleProvider.GetService(type), notificationHandlers);
            var chatNotificationModuleServiceCollection = new ServiceCollection();
            chatNotificationModuleServiceCollection.RegisterDotChat(new TestChatNotificationModule(notificationBus, store, eventNotificationSender));
            _chatNotificationModuleProvider = chatNotificationModuleServiceCollection.BuildServiceProvider();

            var workerHandlers = new List<Type>()
            {
                typeof(ChatsWorker),
                typeof(ChatMessagesWorker),
                typeof(ChatMessageIndexationWorker),
                typeof(ChatParticipantsWorker),
                typeof(ChatSystemMessagesWorker)
            };
            var workerBus = new InMemoryBus(queue, r => r.WorkerQueue, type => _chatWorkerModuleProvider.GetService(type), workerHandlers);
            var chatWorkerModuleServiceCollection = new ServiceCollection();
            chatWorkerModuleServiceCollection.RegisterDotChat(new TestChatWorkerModule(workerBus, store));
            _chatWorkerModuleProvider = chatWorkerModuleServiceCollection.BuildServiceProvider();

            foreach (var user in Users)
            {
                _userContexts.Add(user.Key, new TestUserContext(user.Value.UserId, _chatNotificationModuleProvider.GetService<IDotChat>(), eventNotificationSender, _chatIds));
            }

            NotificationBusTask = notificationBus.Start();
            WorkerBusTask = workerBus.Start();
        }

        public async Task Check(params Task[] checkTasks)
        {
            await await Task.WhenAny(checkTasks.Concat(Enumerable.Repeat(NotificationBusTask, 1)).Concat(Enumerable.Repeat(WorkerBusTask, 1)));
        }
    }
}
