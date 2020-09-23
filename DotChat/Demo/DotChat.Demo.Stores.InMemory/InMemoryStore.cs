namespace K1vs.DotChat.Demo.Stores.InMemory
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.Models.Messages;
    using Models.Participants;

    public class InMemoryStore
    {
        public InMemoryStore(IEnumerable<ChatUser> users)
        {
            foreach (var user in users)
            {
                Users.TryAdd(user.UserId, user);
            }
        }

        public readonly ConcurrentDictionary<Guid, Chat> Chats = new ConcurrentDictionary<Guid, Chat>();

        public PersonalizedChat Personalize(Chat chat, Guid userId)
        {
            var readIndex = chat.Participants.FirstOrDefault(r => r.UserId == userId)?.ReadIndex ?? 0;
            return new PersonalizedChat(chat.Name, chat.Description, chat.PrivacyMode, chat.Version, chat.ChatId, chat.Participants, chat.LastTimestamp, chat.TopIndex, chat.LastMessageId, chat.LastMessageAuthorId, chat.LastChatMessageInfo, readIndex, chat.TopIndex - readIndex);
        }

        public readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, ChatMessage>> Messages = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, ChatMessage>>();

        public readonly ConcurrentDictionary<Guid, ChatUser> Users = new ConcurrentDictionary<Guid, ChatUser>();
    }
}
