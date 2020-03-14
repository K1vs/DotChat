namespace K1vs.DotChat.Basic.NotificationBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Events.Messages;
    using DotChat.NotificationBuilders;
    using DotChat.Notifications.Messages;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Notifications.Messages;

    public class ChatMessagesNotificationBuilder: IChatMessagesNotificationBuilder<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public IChatMessageAddedNotification<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildChatMessageAddedNotification(IChatMessageAddedEvent<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> @event)
        {
            return new ChatMessageAddedNotification(@event.InitiatorUserId, @event.ChatId, @event.Message);
        }

        public IChatMessageEditedNotification<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildChatMessageEditedNotification(IChatMessageEditedEvent<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> @event)
        {
            return new ChatMessageEditedNotification(@event.InitiatorUserId, @event.ChatId, @event.Message);
        }

        public IChatMessageRemovedNotification BuildChatMessageRemovedNotification(IChatMessageRemovedEvent @event)
        {
            return new ChatMessageRemovedNotification(@event.InitiatorUserId, @event.ChatId, @event.MessageId);
        }

        public IChatMessagesReadNotification BuildChatMessagesReadNotification(IChatMessagesReadEvent @event)
        {
            return new ChatMessagesReadNotification(@event.InitiatorUserId, @event.ChatId, @event.Index, @event.Force);
        }
    }
}
