namespace K1vs.DotChat.Basic.Security
{
    using System.Collections.Generic;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Security;
    using Models.Chats;
    using Models.Participants;
    using Stores.Participants;
    using Stores.Users;

    public class ChatsPermissionValidator: ChatsPermissionValidator<List<PersonalizedChat>, PersonalizedChat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        public ChatsPermissionValidator(IReadChatParticipantStore<ChatParticipant> readChatParticipantStore, IReadUserStore<ChatUser> readUserStore) : base(readChatParticipantStore, readUserStore)
        {
        }
    }
}
