namespace K1vs.DotChat.Basic.NotificationBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Events.Chat;
    using DotChat.Events.Chats;
    using DotChat.NotificationBuilders;
    using DotChat.Notifications.Chats;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Chats;
    using Models.Participants;
    using Notifications.Chats;

    public class ChatsNotificationBuilder: IChatsNotificationBuilder<PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public virtual IChatAddedNotification<PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildChatAddedNotification(IChatAddedEvent<Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> @event)
        {
            var personalizedChat = new PersonalizedChat(@event.Chat.Name, @event.Chat.Description, @event.Chat.PrivacyMode, @event.Chat.Version, @event.Chat.ChatId, @event.Chat.Participants, @event.Chat.LastTimestamp, @event.Chat.TopIndex, @event.Chat.LastMessageId, @event.Chat.LastMessageAuthorId, @event.Chat.LastChatMessageInfo, 0, @event.Chat.TopIndex);
            return new ChatAddedNotification(@event.InitiatorUserId, personalizedChat);
        }

        public virtual IChatInfoEditedNotification<ChatInfo> BuildChatInfoEditedNotification(IChatInfoEditedEvent<ChatInfo> @event)
        {
            return new ChatInfoEditedNotification(@event.InitiatorUserId, @event.ChatId, @event.ChatInfo);
        }

        public virtual IChatRemovedNotification<ChatInfo> BuildChatRemovedNotification(IChatRemovedEvent<ChatInfo> @event)
        {
            return new ChatRemovedNotification(@event.InitiatorUserId, @event.ChatId, @event.ChatInfo);
        }
    }
}
