using K1vs.DotChat.Demo.SignalR;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace K1vs.DotChat.Demo.SignalR
{
    using System;
    using System.IO;
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.SignalR;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;
    using K1vs.DotChat.Implementations.Autofac;
    using K1vs.DotChat.Demo.SignalR.Modules;
    using K1vs.DotChat.Demo.Bus.InMemory;
    using K1vs.DotChat.Demo.Stores.InMemory;
    using K1vs.DotChat.Models.Participants;
    using System.Collections.Generic;
    using K1vs.DotChat.Basic.Notifiers;
    using K1vs.DotChat.Basic.Workers;
    using System.Linq;
    using K1vs.DotChat.Services;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Basic.Chats;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.Demo.Stores.InMemory.Chats;
    using K1vs.DotChat.Stores.Chats;
    using System.Security.Claims;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Microsoft.AspNet.SignalR.Infrastructure;
    using K1vs.DotChat.Basic;
    using System.Threading.Tasks;
    using K1vs.DotChat.Implementations.SignalR;

    public class Startup
    {
        public IReadOnlyList<ChatUser> Users = new List<ChatUser>
        {
            new ChatUser(Guid.Parse("99C10789-D258-4093-93ED-7DE74A81E3FA"), "A"),
            new ChatUser(Guid.Parse("3C38BD03-6B08-41DC-8EE1-3074F36193A0"), "B"),
            new ChatUser(Guid.Parse("EA5D4296-B2BF-4C18-A414-3931842B9DE9"), "C"),
            new ChatUser(Guid.Parse("23100590-5D20-487A-B5AD-9986F3EAF702"), "D"),
            new ChatUser(Guid.Parse("F3287C5D-FA20-4DC4-8E0C-092CFCF9A261"), "E")
        };

        public void Configuration(IAppBuilder app)
        {
            app.UseStaticFiles("/wwwroot");

            app.Use((context, next) =>
            {
                var userId = context.Request.Query.Get("userId");
                if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var userIdGuid))
                {
                    context.Authentication.User = new ClaimsPrincipal(new List<ClaimsIdentity>() { 
                        new ClaimsIdentity(new List<Claim>{new Claim("name", userIdGuid.ToString()) }, "Demo", "name", "role")
                    });
                }
                return next.Invoke();
            });

            var builder = new ContainerBuilder();
            IContainer container = null;

            var notificationHandlers = new List<Type>()
            {
                typeof(ChatNotifier),
                typeof(ChatMessageNotifier),
                typeof(ChatParticipantNotifier)
            };

            var workerHandlers = new List<Type>()
            {
                typeof(ChatsWorker),
                typeof(ChatMessagesWorker),
                typeof(ChatMessageIndexationWorker),
                typeof(ChatParticipantsWorker),
                typeof(ChatSystemMessagesWorker)
            };

            var queue = new InMemoryQueues();
            var store = new InMemoryStore(Users);
            var bus = new InMemoryBus(queue, r => r.WorkerQueue, type => container?.Resolve(type), workerHandlers.Concat(notificationHandlers));
            var busTask = bus.Start();

            app.Use((context, next) =>
            {
                if (busTask.IsFaulted)
                {
                    throw busTask.Exception;
                }               
                return next.Invoke();
            });

            builder.RegisterDotChat(new TestChatWorkerModule(bus, store));
            builder.RegisterDotChat(new TestChatNotificationModule());

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new SignalRContractResolver();
            var serializer = JsonSerializer.Create(settings);
            builder.RegisterInstance(serializer).As<JsonSerializer>();

            builder.RegisterType<UserIdProvider>().As<IUserIdProvider>().SingleInstance();

            var config = new HubConfiguration();
            config.EnableDetailedErrors = true;
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
            builder.Register<IConnectionManagerAccessor>(r => new ConnectionManagerAccessor(config.Resolver.Resolve<IConnectionManager>()));

            container = builder.Build();

            var dotChat = container.Resolve<IDotChat>();

            dotChat.Chats.Add(Users[0].UserId, null, new ChatInfo
            {
                Name = "TestChat"
            }, new Basic.Participants.ParticipationCandidates(new List<ParticipationCandidate> {
                new ParticipationCandidate(Users[0].UserId, Participants.ChatParticipantType.Admin),
                new ParticipationCandidate(Users[3].UserId, Participants.ChatParticipantType.Admin)
            }, new List<ParticipationCandidate> { })).ContinueWith(async r => {
                await Task.Delay(TimeSpan.FromSeconds(10));
                await dotChat.ChatParticipants.Append(Users[0].UserId, r.Result, new List<ParticipationCandidate> {
                    new ParticipationCandidate(Users[1].UserId, Participants.ChatParticipantType.Participant),
                    new ParticipationCandidate(Users[2].UserId, Participants.ChatParticipantType.Participant)
                }, new List<ParticipationCandidate>());
            });

            Task.Delay(TimeSpan.FromSeconds(10))
                .ContinueWith((t) =>
                dotChat.Chats.Add(Users[0].UserId, null, new ChatInfo
            {
                Name = "TestChat2"
            }, new Basic.Participants.ParticipationCandidates(new List<ParticipationCandidate> {
                new ParticipationCandidate(Users[0].UserId, Participants.ChatParticipantType.Admin),
                new ParticipationCandidate(Users[1].UserId, Participants.ChatParticipantType.Admin)
            }, new List<ParticipationCandidate> { })));

            config.Resolver = new AutofacDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.MapSignalR("/signalr", config);
        }
    }
}
