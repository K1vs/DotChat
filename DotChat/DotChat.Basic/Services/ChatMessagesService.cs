namespace K1vs.DotChat.Basic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bus;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using DotChat.CommandBuilders;
    using DotChat.Security;
    using DotChat.Services;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Security;
    using Stores.Messages;

    public class ChatMessagesService: 
        ChatMessagesService<ChatServicesConfiguration, ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        public ChatMessagesService(ChatServicesConfiguration chatServicesConfiguration, 
            IChatMessagesPermissionValidator<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions> chatMessagesPermissionValidator, 
            IReadChatMessageStore<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions> readChatMessageStore, 
            IChatMessagesCommandBuilder<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> chatMessagesCommandBuilder, 
            IChatCommandSender chatCommandSender) 
            : base(chatServicesConfiguration, chatMessagesPermissionValidator, readChatMessageStore, chatMessagesCommandBuilder, chatCommandSender)
        {
        }
    }
}
