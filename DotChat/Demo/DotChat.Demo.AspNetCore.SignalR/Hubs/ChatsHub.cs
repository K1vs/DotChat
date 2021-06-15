namespace K1vs.DotChat.Demo.AspNetCore.SignalR.Hubs
{
    using K1vs.DotChat.Basic.Chats;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using Implementations.AspNetCore.SignalR;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.Models.Participants;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Clients;
    using Services;
    using K1vs.DotChat.Basic.Participants;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Models.Messages.Typed;
    using K1vs.DotChat.Basic.Messages.Typed;

    public class ChatsHub : ChatsHub<IChatsClient, PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo,
            List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>,
            ParticipationCandidate, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        public ChatsHub(IChatsService<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions> chatsService) : base(chatsService)
        {
        }
    }
}