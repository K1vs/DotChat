namespace K1vs.DotChat.Basic.Security
{
    using System.Collections.Generic;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Security;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Stores.Messages;
    using Stores.Participants;

    public class ChatMessagesPermissionValidator: ChatMessagesPermissionValidator<ChatInfo, ChatUser, ChatParticipant, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage,
            List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions>
    {
        public ChatMessagesPermissionValidator(IReadChatParticipantStore<ChatParticipant> readChatParticipantStore, IReadChatMessageStore<ChatInfo, ChatUser, List<ChatMessage>, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, MessageFilter, PagedResult<List<ChatMessage>, ChatMessage>, PagingOptions> readChatMessageStore) : base(readChatParticipantStore, readChatMessageStore)
        {
        }
    }
}
