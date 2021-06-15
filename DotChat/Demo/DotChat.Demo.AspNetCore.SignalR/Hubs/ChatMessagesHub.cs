namespace K1vs.DotChat.Demo.AspNetCore.SignalR.Hubs
{
    using Basic.Messages;
    using Basic.Messages.Typed;
    using Clients;
    using Common.Filters;
    using Common.Paging;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Services;
    using Implementations.AspNetCore.SignalR;
    using System.Collections.Generic;

    public class ChatMessagesHub: ChatMessagesHub<IChatMessagesClient, ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo,
        TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment,
        List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter,
        PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        public ChatMessagesHub(IChatMessagesService<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions> chatMessagesService) : base(chatMessagesService)
        {
        }
    }
}