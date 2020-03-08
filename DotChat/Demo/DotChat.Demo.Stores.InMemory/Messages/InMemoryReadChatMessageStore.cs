namespace K1vs.DotChat.Demo.Stores.InMemory.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Basic.Configuration;
    using Basic.Messages;
    using Basic.Messages.Typed;
    using Common.Paging;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Stores.Messages;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class InMemoryReadChatMessageStore: IReadChatMessageStore<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        protected readonly ChatServicesConfiguration ServicesConfiguration;
        protected readonly InMemoryStore Store;
        
        public InMemoryReadChatMessageStore(ChatServicesConfiguration servicesConfiguration, InMemoryStore store)
        {
            ServicesConfiguration = servicesConfiguration;
            Store = store;
        }

        public async Task<PagedResult<List<ChatMessage>, ChatMessage>> Retrieve(Guid chatId, IReadOnlyCollection<MessageFilter> filters, PagingOptions pagingOptions)
        {
            await Task.Yield();
            IEnumerable<ChatMessage> query = Store.Messages[chatId]
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
            return new PagedResult<List<ChatMessage>, ChatMessage>(query.ToList(), skip == 0 ? null : prev.ToString(), $"{skip + take}");
        }

        public async Task<PagedResult<List<ChatMessage>, ChatMessage>> Retrieve(Guid chatId, PagingOptions pagingOptions)
        {
            await Task.Yield();
            IEnumerable<ChatMessage> query = Store.Messages[chatId]
                .Values
                .OrderByDescending(r => r.Index);
            if (!int.TryParse(pagingOptions?.Cursor, out var skip))
            {
                skip = 0;
            }
            var take = pagingOptions?.Limit ?? ServicesConfiguration.DefaultChatsPageSize;
            query = query.Skip(skip).Take(take);
            var prev = Math.Max(skip - take, 0);
            return new PagedResult<List<ChatMessage>, ChatMessage>(query.ToList(), skip == 0 ? null : prev.ToString(), $"{skip + take}");
        }

        public async Task<ChatMessage> Retrieve(Guid chatId, Guid messageId)
        {
            await Task.Yield();
            return Store.Messages[chatId][messageId];
        }
    }
}
