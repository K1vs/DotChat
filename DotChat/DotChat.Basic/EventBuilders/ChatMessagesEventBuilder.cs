namespace K1vs.DotChat.Basic.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.EventBuilders;
    using DotChat.Events.Messages;
    using DotChat.Messages;
    using Events.Messages;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class ChatMessagesEventBuilder: IChatMessagesEventBuilder<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage,
        List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public IChatMessageAddedEvent<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildChatMessageAddedEvent(Guid initiatorUserId, Guid chatId, ChatMessage chatMessage)
        {
            return new ChatMessageAddedEvent(initiatorUserId, chatId, chatMessage);
        }

        public IChatMessageAddedEvent<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildChatMessageAddedEvent(Guid initiatorUserId, Guid chatId, Guid messageId, DateTime timestamp, long index,
            ChatMessageInfo chatMessageInfo)
        {
            var message = new ChatMessage(messageId, timestamp, index, initiatorUserId, MessageStatus.Actual, null, chatMessageInfo);
            return new ChatMessageAddedEvent(initiatorUserId, chatId, message);
        }

        public IChatMessageEditedEvent<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildChatMessageEditedEvent(Guid initiatorUserId, Guid chatId, ChatMessage chatMessage)
        {
            return new ChatMessageEditedEvent(initiatorUserId, chatId, chatMessage);
        }

        public IChatMessageRemovedEvent BuildChatMessageRemovedEvent(Guid initiatorUserId, Guid chatId, Guid chatMessageId, long version)
        {
            return new ChatMessageRemovedEvent(initiatorUserId, chatId, chatMessageId, version);
        }

        public IChatMessagesReadEvent BuildChatMessagesReadEvent(Guid initiatorUserId, Guid chatId, long index)
        {
            return new ChatMessagesReadEvent(initiatorUserId, chatId, index);
        }
    }
}
