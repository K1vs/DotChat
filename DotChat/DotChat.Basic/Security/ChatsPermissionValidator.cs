namespace K1vs.DotChat.Basic.Security
{
    using System.Collections.Generic;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Security;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Chats;
    using Models.Participants;
    using Stores.Participants;
    using Stores.Users;

    public class ChatsPermissionValidator: ChatsPermissionValidator<List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        public ChatsPermissionValidator(IReadChatParticipantStore<ChatParticipant> readChatParticipantStore, IReadUserStore<ChatUser> readUserStore) : base(readChatParticipantStore, readUserStore)
        {
        }
    }
}
