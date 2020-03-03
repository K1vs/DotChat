namespace K1vs.DotChat.Demo.Bus.InMemory
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands;
    using Dependency;
    using DotChat.Bus;
    using Events;
    using FrameworkUtils.Extensions;
    using Handlers;

    public class InMemoryBus: IChatCommandSender, IChatEventPublisher
    {
        private readonly Func<Type, object> _handlerResolver;
        private readonly Dictionary<Type, List<Type>> _handlers;
        private readonly ChatBusContext _chatBusContext;
        private readonly InMemoryQueues _inMemoryQueues;
        private readonly Func<InMemoryQueues, BlockingCollection<object>> _readQueue;
        private Task _readQueueTask;

        public InMemoryBus(InMemoryQueues inMemoryQueues, Func<InMemoryQueues, BlockingCollection<object>> readQueue, Func<Type, object> handlerResolver, IEnumerable<Type> handlerTypes)
        {
            _handlerResolver = handlerResolver;
            _handlers = CreateHandlers(handlerTypes);
            _chatBusContext = new ChatBusContext(this);
            _inMemoryQueues = inMemoryQueues;
            _readQueue = readQueue;
        }

        public Task Start()
        {
            _readQueueTask = ReadQueue();
            return _readQueueTask;
        }

        public Task Send<TCommand>(TCommand command) where TCommand : ICommandBase
        {
            _inMemoryQueues.WorkerQueue.Add(command);
            return Task.CompletedTask;
        }

        public Task Publish<TEvent>(TEvent @event) where TEvent : IEventBase
        {
            _inMemoryQueues.WorkerQueue.Add(@event);
            _inMemoryQueues.NotificationQueue.Add(@event);
            return Task.CompletedTask;
        }

        private Task ReadQueue()
        {
            TaskCompletionSource<byte> tcs = new TaskCompletionSource<byte>();

            async void ReadQueueCore()
            {
                try
                {
                    foreach (var message in _readQueue(_inMemoryQueues).GetConsumingEnumerable())
                    {
                        await CallHandler(message);
                    }
                    tcs.SetResult(0);
                }
                catch (TaskCanceledException)
                {
                    tcs.SetCanceled();
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            }

            ThreadPool.QueueUserWorkItem(o => ReadQueueCore());

            return tcs.Task;
        }

        private async Task CallHandler(object message)
        {
            var messageClassType = message.GetType();
            var messageTypes = messageClassType.GetBaseClasses(true);
            var messageInterfaces = messageClassType.GetInterfaces();
            foreach (var messageType in messageTypes.Concat(messageInterfaces))
            {
                if (_handlers.TryGetValue(messageType, out var messageHandlerTypes))
                {
                    foreach (var messageHandlerType in messageHandlerTypes)
                    {
                        var handler = _handlerResolver(messageHandlerType);
                        var task = (Task)messageHandlerType.GetMethod("Handle", new Type[] { messageType, typeof(IChatBusContext) })?
                            .Invoke(handler, new object[] { message, _chatBusContext });
                        if (task != null)
                        {
                            await task;
                        }
                    }
                }
            }
        }

        private Dictionary<Type, List<Type>> CreateHandlers(IEnumerable<Type> handlerTypes)
        {
            Dictionary<Type, List<Type>> result = new Dictionary<Type, List<Type>>();
            foreach (var handlerType in handlerTypes)
            {
                var messageTypes = handlerType.GetInterfaces()
                    .Where(r => r.IsConstructedGenericType &&
                                (r.GetGenericTypeDefinition() == typeof(IHandleCommand<>) ||
                                 r.GetGenericTypeDefinition() == typeof(IHandleEvent<>)))
                    .Select(r => r.GetGenericArguments()[0]).ToList();
                foreach (var @interface in messageTypes)
                {
                    if (result.TryGetValue(@interface, out var interfaceHandlerType))
                    {
                        interfaceHandlerType.Add(handlerType);
                    }
                    else
                    {
                        result.Add(@interface, new List<Type>{handlerType});
                    }
                }
            }

            return result;
        }
    }
}
