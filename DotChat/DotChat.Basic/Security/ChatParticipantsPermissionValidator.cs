namespace K1vs.DotChat.Basic.Security
{
    using System.Collections.Generic;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Security;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Chats;
    using Models.Participants;
    using Stores.Chats;
    using Stores.Participants;

    public class ChatParticipantsPermissionValidator: ChatParticipantsPermissionValidator<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, List<ParticipationCandidate>, ParticipationCandidate, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions>
    {
        public ChatParticipantsPermissionValidator(IReadChatParticipantStore<ChatParticipant> readChatParticipantStore, IReadChatStore<PersonalizedChatsSummary, List<PersonalizedChat>, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage, ChatFilter<ChatUserFilter, MessageFilter>, ChatUserFilter, MessageFilter, PagedResult<List<PersonalizedChat>, PersonalizedChat>, PagingOptions> readChatStore) : base(readChatParticipantStore, readChatStore)
        {
        }
    }
}
