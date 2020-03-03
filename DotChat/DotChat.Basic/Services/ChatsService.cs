namespace K1vs.DotChat.Basic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using DotChat.CommandBuilders;
    using DotChat.Security;
    using DotChat.Services;
    using Models.Chats;
    using Models.Participants;
    using Participants;
    using Security;
    using Stores.Chats;

    public class ChatsService: ChatsService<ChatServicesConfiguration, PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        public ChatsService(ChatServicesConfiguration chatServicesConfiguration, IChatsPermissionValidator<List<PersonalizedChat>, PersonalizedChat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions> chatsPermissionValidator, IReadChatStore<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, List<ChatParticipant>, ChatParticipant, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions> readChatStore, IChatsCommandBuilder<ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate> chatsCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration, chatsPermissionValidator, readChatStore, chatsCommandBuilder, chatCommandSender)
        {
        }
    }
}
