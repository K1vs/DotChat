namespace K1vs.DotChat.Demo.Stores.InMemory.Messages
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Basic.Configuration;
    using Basic.Messages;
    using Basic.Messages.Typed;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Messages;
    using K1vs.DotChat.Stores.Messages;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class InMemoryChatMessageStore: InMemoryReadChatMessageStore, IChatMessageStore<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        public InMemoryChatMessageStore(ChatServicesConfiguration servicesConfiguration, InMemoryStore store) : base(servicesConfiguration, store)
        {
        }

        public async Task Read(Guid chatId, Guid userId,  long index)
        {
            await Task.Yield();
            var p = Store.Chats[chatId].Participants.FirstOrDefault(r => r.UserId == userId);
            p.ReadIndex = index;
        }

        public async Task<ChatMessage> Create(Guid chatId, Guid userId, Guid messageId, ChatMessageInfo messageInfo, DateTime timestamp, long index, bool isSystem, Guid creatorId)
        {
            await Task.Yield();
            var message = new ChatMessage(messageId, timestamp, index, userId, MessageStatus.Actual, null, isSystem, messageInfo);
            Store.Messages.AddOrUpdate(chatId, k => new ConcurrentDictionary<Guid, ChatMessage>(Enumerable.Repeat(new KeyValuePair<Guid, ChatMessage>(message.MessageId, message), 1)), (k, ov) =>
                {
                    ov.TryAdd(message.MessageId, message);
                    return ov;
                });
            return message;
        }

        public async Task<ChatMessage> Update(Guid chatId, Guid messageId, ChatMessageInfo messageInfo, Guid modifierId)
        {
            await Task.Yield();
            var message = Store.Messages[chatId][messageId];
            message.Type = messageInfo.Type;
            message.Immutable = messageInfo.Immutable;
            message.Style = messageInfo.Style;
            message.Metadata = messageInfo.Metadata;

            message.Text = messageInfo.Text;
            message.Quote = messageInfo.Quote;
            message.MessageAttachments = messageInfo.MessageAttachments;
            message.ChatRefs = messageInfo.ChatRefs;
            message.Contacts = messageInfo.Contacts;
            message.Version += 1;
            return message;
        }

        public async Task<ChatMessage> Delete(Guid chatId, Guid messageId, Guid removerId)
        {
            await Task.Yield();
            Store.Messages[chatId].TryRemove(messageId, out var chatMessage);
            chatMessage.Version += 1;
            return chatMessage;
        }

        public async Task Archive(Guid chatId, Guid originalMessageId, Guid achievedMessageId, ChatMessage messageInfo, Guid archiverId)
        {
            await Task.Yield();
        }
    }
}
