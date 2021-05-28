namespace K1vs.DotChat.Demo.Stores.InMemory.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Paging;
    using K1vs.DotChat.Common.Configuration;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Models.Messages;
    using K1vs.DotChat.Stores.Messages;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class InMemoryReadChatMessageStore: IReadChatMessageStore
    {
        protected readonly ChatServicesConfiguration ServicesConfiguration;
        protected readonly InMemoryStore Store;
        
        public InMemoryReadChatMessageStore(ChatServicesConfiguration servicesConfiguration, InMemoryStore store)
        {
            ServicesConfiguration = servicesConfiguration;
            Store = store;
        }

        public async Task<IPagedResult<IChatMessage>> Retrieve(Guid chatId, IReadOnlyCollection<IChatMessageFilter> filters, IPagingOptions pagingOptions)
        {
            await Task.Yield();
            if(!Store.Messages.TryGetValue(chatId, out var messages))
            {
                return new PagedResult<ChatMessage>(new List<ChatMessage>(), null, null);
            }
            IEnumerable<ChatMessage> query = messages
                .Values
                .Where(r => filters.Any(filter => filter.Search == null || r.Text.Content.Contains(filter.Search)))
                .OrderByDescending(r => r.Index);
            if (!int.TryParse(pagingOptions?.Cursor, out var skip))
            {
                skip = 0;
            }
            var take = pagingOptions?.Limit ?? ServicesConfiguration.DefaultChatsPageSize;
            query = query.Skip(skip).Take(take);
            var prev = Math.Max(skip - take, 0);
            return new PagedResult<ChatMessage>(query.ToList(), skip == 0 ? null : prev.ToString(), $"{skip + take}");
        }

        public async Task<IPagedResult<IChatMessage>> Retrieve(Guid chatId, IPagingOptions pagingOptions)
        {
            await Task.Yield();
            if (!Store.Messages.TryGetValue(chatId, out var messages))
            {
                return new PagedResult<ChatMessage>(new List<ChatMessage>(), null, null);
            }
            IEnumerable<ChatMessage> query = messages
                .Values
                .OrderByDescending(r => r.Index);
            if (!int.TryParse(pagingOptions?.Cursor, out var skip))
            {
                skip = 0;
            }
            var take = pagingOptions?.Limit ?? ServicesConfiguration.DefaultChatsPageSize;
            query = query.Skip(skip).Take(take);
            var prev = Math.Max(skip - take, 0);
            return new PagedResult<ChatMessage>(query.ToList(), skip == 0 ? null : prev.ToString(), $"{skip + take}");
        }

        public async Task<IChatMessage> Retrieve(Guid chatId, Guid messageId)
        {
            await Task.Yield();
            if (!Store.Messages.TryGetValue(chatId, out var messages))
            {
                return null;
            }
            return messages[messageId];
        }
    }
}
