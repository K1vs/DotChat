namespace K1vs.DotChat.Basic.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Chats;
    using DotChat.EventBuilders;
    using DotChat.Events.Chat;
    using DotChat.Events.Chats;
    using Events.Chat;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Chats;
    using Models.Participants;

    public class ChatsEventBuilder: IChatsEventBuilder<Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public IChatAddedEvent<Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildChatAddedEvent(Guid initiatorUserId, Chat chat)
        {
            return new ChatAddedEvent(initiatorUserId, chat);
        }

        public IChatInfoEditedEvent<ChatInfo> BuildChatInfoEditedEvent(Guid initiatorUserId, Guid chatId, ChatInfo chatInfo)
        {
            return new ChatInfoEditedEvent(initiatorUserId, chatId, chatInfo);
        }

        public IChatRemovedEvent<ChatInfo> BuildChatRemovedEvent(Guid initiatorUserId, Guid chatId, ChatInfo chatInfo)
        {
            return new ChatRemovedEvent(initiatorUserId, chatId, chatInfo);
        }
    }
}
