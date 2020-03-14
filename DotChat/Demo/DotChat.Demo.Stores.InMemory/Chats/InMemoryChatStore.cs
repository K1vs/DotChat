namespace K1vs.DotChat.Demo.Stores.InMemory.Chats
{
    using K1vs.DotChat.Stores.Chats;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Basic.Chats;
    using Basic.Configuration;
    using Basic.Participants;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Participants;
    using Models.Chats;
    using Models.Participants;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Models.Messages.Typed;
    using K1vs.DotChat.Basic.Messages.Typed;

    public class InMemoryChatStore: InMemoryReadChatStore, IChatStore<PersonalizedChatsSummary, List<PersonalizedChat>,PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, List<ParticipationCandidate>, ParticipationCandidate, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        public InMemoryChatStore(ChatServicesConfiguration servicesConfiguration, InMemoryStore store) : base(servicesConfiguration, store)
        {
        }

        public async Task<Chat> Create(Guid chatId, ChatInfo chatInfo, List<ParticipationCandidate> toAdd, List<ParticipationCandidate> toInvite, Guid creatorId)
        {
            await Task.Yield();
            var now = DateTime.UtcNow;
            var added = toAdd.Select(r =>
                new ChatParticipant(r.ChatParticipantType, ChatParticipantStatus.Active, now, -1, 0, Store.Users[r.UserId]));
            var invited = toInvite.Select(r =>
                new ChatParticipant(r.ChatParticipantType, ChatParticipantStatus.Active, now, -1, 0, Store.Users[r.UserId]));
            var chat = new Chat(chatInfo, chatId, added.Concat(invited).ToList(), now, 0, null, null, null);
            Store.Chats.TryAdd(chatId, chat);
            return chat;
        }

        public async Task<ChatInfo> UpdateInfo(Guid chatId, ChatInfo chatInfo, Guid modifierId)
        {
            await Task.Yield();
            var chat = Store.Chats[chatId];
            chat.Name = chatInfo.Name;
            chat.Description = chatInfo.Description;
            chat.PrivacyMode = chatInfo.PrivacyMode;
            chat.Version += 1;
            return chat;
        }

        public async Task<ChatInfo> Delete(Guid chatId, Guid removerId)
        {
            await Task.Yield();
            Store.Chats.TryRemove(chatId, out var chat);
            chat.Version += 1;
            return chat;
        }

        public async Task SetTop(Guid chatId, ChatMessage topChatMessage)
        {
            await Task.Yield();
            var chat = Store.Chats[chatId];
            lock (chat)
            {
                if (chat.TopIndex < topChatMessage.Index)
                {
                    chat.LastTimestamp = topChatMessage.Timestamp;
                    chat.TopIndex = topChatMessage.Index;
                    chat.LastMessageId = topChatMessage.MessageId;
                    chat.LastMessageAuthorId = topChatMessage.AuthorId;
                    chat.LastChatMessageInfo = new ChatMessageInfo(topChatMessage.Type, topChatMessage.Version, topChatMessage.Immutable, topChatMessage.Style, topChatMessage.Metadata,
                        topChatMessage.Text, topChatMessage.Quote, topChatMessage.MessageAttachments, topChatMessage.ChatRefs, topChatMessage.Contacts);
                }
            }
        }
    }
}
