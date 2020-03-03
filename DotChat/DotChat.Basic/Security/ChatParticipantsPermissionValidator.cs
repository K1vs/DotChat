namespace K1vs.DotChat.Basic.Security
{
    using System.Collections.Generic;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Security;
    using Models.Chats;
    using Models.Participants;
    using Stores.Chats;
    using Stores.Participants;

    public class ChatParticipantsPermissionValidator: ChatParticipantsPermissionValidator<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, List<ChatParticipant>, ChatParticipant, List<ParticipationCandidate>, ParticipationCandidate, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        public ChatParticipantsPermissionValidator(IReadChatParticipantStore<ChatParticipant> readChatParticipantStore, IReadChatStore<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, List<ChatParticipant>, ChatParticipant, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions> readChatStore) : base(readChatParticipantStore, readChatStore)
        {
        }
    }
}
