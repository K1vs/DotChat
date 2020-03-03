namespace K1vs.DotChat.Basic.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using DotChat.EventBuilders;
    using DotChat.Security;
    using DotChat.Workers;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Security;
    using Stores.Messages;

    public class ChatMessagesWorker : ChatMessagesWorker<ChatWorkersConfiguration, ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        public ChatMessagesWorker(ChatWorkersConfiguration chatWorkersConfiguration, IChatMessagesPermissionValidator<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions> chatMessagesPermissionValidator, IChatMessageStore<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions> chatMessageStore, IChatMessagesEventBuilder<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> chatMessagesEventBuilder) : base(chatWorkersConfiguration, chatMessagesPermissionValidator, chatMessageStore, chatMessagesEventBuilder)
        {
        }
    }
}
