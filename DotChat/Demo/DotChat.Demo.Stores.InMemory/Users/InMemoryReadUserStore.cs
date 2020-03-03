namespace K1vs.DotChat.Demo.Stores.InMemory.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DotChat.Participants;
    using DotChat.Stores.Users;
    using Models.Participants;

    public class InMemoryReadUserStore: IReadUserStore<ChatUser>
    {
        protected readonly InMemoryStore Store;

        public InMemoryReadUserStore(InMemoryStore store)
        {
            Store = store;
        }

        public async Task<ChatUser> Retrieve(Guid userId)
        {
            await Task.Yield();
            return Store.Users.TryGetValue(userId, out var user) ? user : null;
        }

        public async Task<IReadOnlyCollection<ChatUser>> Retrieve(IEnumerable<Guid> userIds)
        {
            return await Task.WhenAll(userIds.Select(Retrieve).ToArray());
        }

        public ChatUser Customize(ChatUser user, string style, string metadata)
        {
            return new ChatUser(user.UserId, user.Name, user.Details, user.AvatarId, user.InviteOnly, user.CanCreateChat, style ?? user.Style, metadata ?? user.Style);
        }
    }
}
