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
    using DotChat.CommandBuilders;
    using DotChat.EventBuilders;
    using DotChat.Generators;
    using DotChat.Security;
    using DotChat.Workers;
    using Generators;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Security;

    public class ChatMessageIndexationWorker: ChatMessageIndexationWorker<ChatWorkersConfiguration, ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        public ChatMessageIndexationWorker(ChatWorkersConfiguration chatWorkersConfiguration, IChatMessagesPermissionValidator<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions> chatMessagesPermissionValidator, IChatMessageTimestampGenerator chatMessageTimestampGenerator, IChatMessageIndexGenerator messageIndexGenerator, IChatMessagesEventBuilder<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> chatMessagesEventBuilder, IChatMessagesCommandBuilder<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> chatMessagesCommandBuilder) : base(chatWorkersConfiguration, chatMessagesPermissionValidator, chatMessageTimestampGenerator, messageIndexGenerator, chatMessagesEventBuilder, chatMessagesCommandBuilder)
        {
        }
    }
}
