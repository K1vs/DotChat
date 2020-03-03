namespace K1vs.DotChat.Basic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using DotChat.Services;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Participants;

    public class DotChatFacade: DotChatFacade<ChatServicesConfiguration, PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, List<ChatParticipant>, ChatParticipant, ChatUser,
        List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage,
        ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>, IDotChat
    {
        public DotChatFacade(IChatsService<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions> chats, IChatParticipantsService<List<ParticipationCandidate>, ParticipationCandidate> chatParticipants, IChatMessagesService<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions> chatMessages) : base(chats, chatParticipants, chatMessages)
        {
        }
    }
}
