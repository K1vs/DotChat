namespace K1vs.DotChat.Demo.Stores.InMemory.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DotChat.Stores.Participants;
    using Models.Participants;

    public class InMemoryReadChatParticipantStore: IReadChatParticipantStore<ChatParticipant>
    {
        protected readonly InMemoryStore Store;

        public InMemoryReadChatParticipantStore(InMemoryStore store)
        {
            Store = store;
        }

        public async Task<IReadOnlyCollection<Guid>> RetrieveIds(Guid chatId)
        {
            await Task.Yield();
            return Store.Chats[chatId].Participants.Select(r => r.UserId).ToList();
        }

        public async Task<IReadOnlyCollection<ChatParticipant>> RetrieveList(Guid chatId, IEnumerable<Guid> participantsIds)
        {
            await Task.Yield();
            return Store.Chats[chatId].Participants.Where(r => participantsIds.Contains(r.UserId)).ToList();
        }

        public async Task<ChatParticipant> Retrieve(Guid chatId, Guid userId)
        {
            await Task.Yield();
            return Store.Chats[chatId].Participants.FirstOrDefault(r => r.UserId == userId);
        }
    }
}
