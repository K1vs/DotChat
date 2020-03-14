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
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Chats;
    using Models.Participants;
    using Participants;
    using Security;
    using Stores.Chats;

    public class ChatsService: ChatsService<ChatServicesConfiguration, PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        public ChatsService(ChatServicesConfiguration chatServicesConfiguration, IChatsPermissionValidator<List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions> chatsPermissionValidator, IReadChatStore<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions> readChatStore, IChatsCommandBuilder<ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate> chatsCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration, chatsPermissionValidator, readChatStore, chatsCommandBuilder, chatCommandSender)
        {
        }
    }
}
