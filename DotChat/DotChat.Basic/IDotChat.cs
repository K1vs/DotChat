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
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Participants;

    public interface IDotChat: IDotChat<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, List<ChatParticipant>, ChatParticipant, ChatUser,
        List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage,
        ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
    }
}
