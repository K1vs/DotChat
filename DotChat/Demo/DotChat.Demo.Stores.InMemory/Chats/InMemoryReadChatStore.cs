namespace K1vs.DotChat.Demo.Stores.InMemory.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Basic.Chats;
    using Basic.Configuration;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Chats;
    using DotChat.Participants;
    using DotChat.Stores.Chats;
    using Models.Chats;
    using Models.Participants;

    public class InMemoryReadChatStore: IReadChatStore<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, List<ChatParticipant>, ChatParticipant, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        protected readonly ChatServicesConfiguration ServicesConfiguration;
        protected readonly InMemoryStore Store;

        public InMemoryReadChatStore(ChatServicesConfiguration servicesConfiguration, InMemoryStore store)
        {
            ServicesConfiguration = servicesConfiguration;
            Store = store;
        }

        public async Task<PersonalizedChatsSummary> RetrievePersonalizedSummary(Guid userId)
        {
            await Task.Yield();
            var chats = GetUserChats(userId)
                .Select(r => Store.Personalize(r.Value, userId))
                .ToList();
            return new PersonalizedChatsSummary
            {
                UnreadMessagesCount = chats.Sum(r => r.UnreadCount),
                UnreadChatsCount = chats.Count(r => r.UnreadCount > 0)
            };
        }

        public async Task<IReadOnlyCollection<Guid>> RetrieveAllIds(Guid userId)
        {
            await Task.Yield();
            return GetUserChats(userId)
                .Select(r => r.Key)
                .ToList();
        }

        public async Task<PagedResult<List<PersonalizedChat>, PersonalizedChat>> RetrievePersonalizedPage(Guid userId, ChatFilter<ChatUserFilter, MessageFilter> filter, PagingOptions pagingOptions)
        {
            await Task.Yield();
            IEnumerable<PersonalizedChat> query = Store.Chats
                .Select(r => Store.Personalize(r.Value, userId))
                .Where(r => filter.Search == null || r.Name.Contains(filter.Search) || filter.SearchInDescription && r.Description.Contains(filter.Search))
                .Where(r => r.Participants.Any(p => filter.UserFiltersList.Any(uFilter =>
                    (uFilter.UserId == null || uFilter.UserId == p.UserId) &&
                    (uFilter.ChatPrivacyMode == null || uFilter.ChatPrivacyMode == r.PrivacyMode) &&
                    (uFilter.ParticipantStatus == null || uFilter.ParticipantStatus == p.ChatParticipantStatus) && 
                    (uFilter.ParticipantType == null || uFilter.ParticipantType == p.ChatParticipantType))))
                .OrderByDescending(r => r.LastTimestamp);
            if (!int.TryParse(pagingOptions?.Cursor, out var skip))
            {
                skip = 0;
            }
            var take = pagingOptions?.Limit ?? ServicesConfiguration.DefaultChatsPageSize;
            query = query.Skip(skip).Take(take);
            var prev = Math.Max(skip - take, 0);
            return new PagedResult<List<PersonalizedChat>, PersonalizedChat>(query.ToList(), skip == 0 ? null : prev.ToString() , $"{skip + take}");
        }

        public async Task<PagedResult<List<PersonalizedChat>, PersonalizedChat>> RetrievePersonalizedPage(Guid userId, PagingOptions pagingOptions)
        {
            await Task.Yield();
            IEnumerable<PersonalizedChat> query = Store.Chats
                .Where(r => r.Value.Participants.Any(e => e.UserId == userId))
                .Select(r => Store.Personalize(r.Value, userId))
                .OrderByDescending(r => r.LastTimestamp);
            if (!int.TryParse(pagingOptions?.Cursor, out var skip))
            {
                skip = 0;
            }
            var take = pagingOptions?.Limit ?? ServicesConfiguration.DefaultChatsPageSize;
            query = query.Skip(skip).Take(take);
            var prev = Math.Max(skip - take, 0);
            return new PagedResult<List<PersonalizedChat>, PersonalizedChat>(query.ToList(), skip == 0 ? null : prev.ToString(), $"{skip + take}");
        }

        public async Task<PersonalizedChat> RetrievePersonalized(Guid chatId, Guid userId)
        {
            await Task.Yield();
            return Store.Personalize(Store.Chats[chatId], userId);
        }

        public async Task<Chat> Retrieve(Guid chatId)
        {
            await Task.Yield();
            return Store.Chats[chatId];
        }

        private IEnumerable<KeyValuePair<Guid, Chat>> GetUserChats(Guid userId)
        {
            return Store.Chats
                .Where(r => r.Value.Participants.Any(p => p.UserId == userId));
        }
    }
}
