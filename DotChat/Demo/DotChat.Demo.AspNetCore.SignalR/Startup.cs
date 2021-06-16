using K1vs.DotChat.Basic;
using K1vs.DotChat.Basic.Notifiers;
using K1vs.DotChat.Basic.Workers;
using K1vs.DotChat.Demo.AspNetCore.SignalR.Hubs;
using K1vs.DotChat.Demo.Bus.InMemory;
using K1vs.DotChat.Demo.Stores.InMemory;
using K1vs.DotChat.Models.Chats;
using K1vs.DotChat.Models.Participants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using K1vs.DotChat.Implementations.DependencyCore;
using K1vs.DotChat.Demo.AspNetCore.SignalR.Modules;

namespace K1vs.DotChat.Demo.AspNetCore.SignalR
{
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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(r =>
            {
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
                var bus = new InMemoryBus(queue, r => r.WorkerQueue, type => r.GetService(type), workerHandlers.Concat(notificationHandlers));
                return bus;   
            });

            services.AddSingleton(r => 
            {
                var store = new InMemoryStore(Users);
                return store;
            });

            services.RegisterDotChat(new TestChatWorkerModule());

            services.RegisterDotChat(new TestChatNotificationModule());

            services.AddSingleton<IUserIdProvider, UserIdProvider>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            var bus = app.ApplicationServices.GetService<InMemoryBus>();
            var busTask = bus.Start();
            app.Use(async (context, next) =>
            {
                if (busTask.IsFaulted)
                {
                    throw busTask.Exception;
                }
                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Query.TryGetValue("access_token", out var userIdValues))
                {
                    var userId = userIdValues.Single();
                    if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var userIdGuid))
                    {
                        context.User = new ClaimsPrincipal(new List<ClaimsIdentity>() {
                            new ClaimsIdentity(new List<Claim>{new Claim("name", userIdGuid.ToString()) }, "Demo", "name", "role")
                        });
                    }
                }
                await next.Invoke();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatsHub>("/chatsHub");
                endpoints.MapHub<ChatParticipantsHub>("/chatParticipantsHub");
                endpoints.MapHub<ChatMessagesHub>("/chatMessagesHub");
            });

            var dotChat = app.ApplicationServices.GetService<IDotChat>();
            AddTestData(dotChat);
        }

        private void AddTestData(IDotChat dotChat)
        {
            dotChat.Chats.Add(Users[0].UserId, null, new ChatInfo
            {
                Name = "TestChat"
            }, new Basic.Participants.ParticipationCandidates(new List<ParticipationCandidate> {
                new ParticipationCandidate(Users[0].UserId, Participants.ChatParticipantType.Admin),
                new ParticipationCandidate(Users[3].UserId, Participants.ChatParticipantType.Admin)
            }, new List<ParticipationCandidate> { })).ContinueWith(async r =>
            {
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

            foreach (var item in Enumerable.Range(3, 10))
            {
                dotChat.Chats.Add(Users[0].UserId, null, new ChatInfo
                {
                    Name = $"TestChat{item}"
                }, new Basic.Participants.ParticipationCandidates(new List<ParticipationCandidate> {
                    new ParticipationCandidate(Users[0].UserId, Participants.ChatParticipantType.Admin),
                    new ParticipationCandidate(Users[3].UserId, Participants.ChatParticipantType.Admin)
                }, new List<ParticipationCandidate> { }));
            }
        }
    }
}
