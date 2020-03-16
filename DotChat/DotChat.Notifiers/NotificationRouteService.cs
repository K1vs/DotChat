namespace K1vs.DotChat.Notifiers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using Participants;
    using Stores.Participants;

    public class NotificationRouteService<TChatNotificationsConfiguration, TChatParticipant> : INotificationRouteService, IDisposable
        where TChatNotificationsConfiguration : IChatNotificationsConfiguration
        where TChatParticipant : IChatParticipant
    {
        protected readonly TimeSpan CleanUpInterval;
        protected readonly ConcurrentDictionary<Guid, Task<ConcurrentDictionary<Guid, bool>>> Chats = new ConcurrentDictionary<Guid, Task<ConcurrentDictionary<Guid, bool>>>();
        protected readonly IReadChatParticipantStore<TChatParticipant> ReadChatParticipantStore;
        protected readonly Timer CleanupTimer;

        public NotificationRouteService(IReadChatParticipantStore<TChatParticipant> readChatParticipantStore, TChatNotificationsConfiguration notificationsConfiguration)
        {
            ReadChatParticipantStore = readChatParticipantStore;
            CleanUpInterval = notificationsConfiguration.CleanUpInterval;
            CleanupTimer = CreateTimer();
        }

        public virtual async Task<IEnumerable<Guid>> GetChatUsers(Guid chatId)
        {
            return (await Chats.GetOrAdd(chatId, GetChatUsersDictionary)).Select(r => r.Key);
        }

        public virtual Task ConnectUser(Guid userId, IEnumerable<Guid> chats)
        {
            return Task.CompletedTask;
        }

        public virtual Task DisconnectUser(Guid userId)
        {
            return Task.CompletedTask;
        }

        public virtual async Task AddUserToChat(Guid userId, Guid chatId)
        {
            var users = await Chats.GetOrAdd(chatId, GetChatUsersDictionary);
            users.TryAdd(userId, false);
        }

        public virtual async Task AddUsersToChat(IEnumerable<Guid> userIds, Guid chatId)
        {
            var users = await Chats.GetOrAdd(chatId, GetChatUsersDictionary);
            foreach (var userId in userIds)
            {
                users.TryAdd(userId, false);
            }
        }

        public virtual Task RemoveUserFromChat(Guid userId, Guid chatId)
        {
            return Task.CompletedTask;
        }

        public virtual Task RemoveUsersFromChat(IEnumerable<Guid> userIds, Guid chatId)
        {
            return Task.CompletedTask;
        }

        public virtual Task RemoveChat(Guid chatId)
        {
            Chats.TryRemove(chatId, out _);
            return Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            CleanupTimer?.Dispose();
        }

        protected virtual async Task<ConcurrentDictionary<Guid, bool>> GetChatUsersDictionary(Guid chatId)
        {
            var userIds = await ReadChatParticipantStore.RetrieveIds(chatId);
            return new ConcurrentDictionary<Guid, bool>(userIds.Select(r => new KeyValuePair<Guid, bool>(r, false)));
        }

        protected virtual Timer CreateTimer()
        {
            return new Timer((state) => Chats.Clear(), null, CleanUpInterval, CleanUpInterval);
        }
    }
}
