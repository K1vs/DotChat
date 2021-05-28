﻿namespace K1vs.DotChat.Demo.SignalR.Hubs
{
    using K1vs.DotChat.Basic.Chats;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Implementations.SignalR;
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

    public class ChatsHub : ChatsHub<IChatsClient, ChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo,
            List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>,
            ParticipationCandidate, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        public ChatsHub(IChatsService<ChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions> chatsService) : base(chatsService)
        {
        }
    }
}