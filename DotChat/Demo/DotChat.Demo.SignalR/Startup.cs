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

    public class Startup
    {
        public IReadOnlyCollection<ChatUser> Users = new List<ChatUser>
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

            builder.RegisterDotChat(new TestChatWorkerModule(bus, store));
            builder.RegisterDotChat(new TestChatNotificationModule());

            var config = new HubConfiguration();
            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            container = builder.Build();

//            try
//            {
//                var t = container.Resolve<IChatsService<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>>();
//                var test = container.Resolve<IChatsService<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo,
//List<ChatParticipant>, ChatParticipant, ParticipationCandidates<List<ParticipationCandidate>, ParticipationCandidate>, List<ParticipationCandidate>,
//ParticipationCandidate, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>>();
//            }
//            catch(Exception ex)
//            {
//                var a = ex;
//            }

            config.Resolver = new AutofacDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.MapSignalR("/signalr", config);
        }
    }
}
