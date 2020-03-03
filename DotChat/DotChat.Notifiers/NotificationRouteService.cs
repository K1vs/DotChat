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
        private readonly TimeSpan _cleanUpInterval;
        private readonly ConcurrentDictionary<Guid, Task<ConcurrentDictionary<Guid, bool>>> _chats = new ConcurrentDictionary<Guid, Task<ConcurrentDictionary<Guid, bool>>>();
        private readonly IReadChatParticipantStore<TChatParticipant> _readChatParticipantStore;
        private readonly Timer _cleanupTimer;

        public NotificationRouteService(IReadChatParticipantStore<TChatParticipant> readChatParticipantStore, TChatNotificationsConfiguration notificationsConfiguration)
        {
            _readChatParticipantStore = readChatParticipantStore;
            _cleanUpInterval = notificationsConfiguration.CleanUpInterval;
            _cleanupTimer = CreateTimer();
        }

        public async Task<IEnumerable<Guid>> GetChatUsers(Guid chatId)
        {
            return (await _chats.GetOrAdd(chatId, GetChatUsersDictionary)).Select(r => r.Key);
        }

        public Task ConnectUser(Guid userId, IEnumerable<Guid> chats)
        {
            return Task.CompletedTask;
        }

        public Task DisconnectUser(Guid userId)
        {
            return Task.CompletedTask;
        }

        public async Task AddUserToChat(Guid userId, Guid chatId)
        {
            var users = await _chats.GetOrAdd(chatId, GetChatUsersDictionary);
            users.TryAdd(userId, false);
        }

        public async Task AddUsersToChat(IEnumerable<Guid> userIds, Guid chatId)
        {
            var users = await _chats.GetOrAdd(chatId, GetChatUsersDictionary);
            foreach (var userId in userIds)
            {
                users.TryAdd(userId, false);
            }
        }

        public Task RemoveUserFromChat(Guid userId, Guid chatId)
        {
            return Task.CompletedTask;
        }

        public Task RemoveUsersFromChat(IEnumerable<Guid> userIds, Guid chatId)
        {
            return Task.CompletedTask;
        }

        public Task RemoveChat(Guid chatId)
        {
            _chats.TryRemove(chatId, out _);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _cleanupTimer?.Dispose();
        }

        private async Task<ConcurrentDictionary<Guid, bool>> GetChatUsersDictionary(Guid chatId)
        {
            var userIds = await _readChatParticipantStore.RetrieveIds(chatId);
            return new ConcurrentDictionary<Guid, bool>(userIds.Select(r => new KeyValuePair<Guid, bool>(r, false)));
        }

        private Timer CreateTimer()
        {
            return new Timer((state) => _chats.Clear(), null, _cleanUpInterval, _cleanUpInterval);
        }
    }
}
