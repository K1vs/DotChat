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
    using Models.Chats;
    using Models.Participants;
    using Notifications.Chats;

    public class ChatsNotificationBuilder: IChatsNotificationBuilder<PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant>
    {
        public IChatAddedNotification<PersonalizedChat, List<ChatParticipant>, ChatParticipant> BuildChatAddedNotification(IChatAddedEvent<Chat, List<ChatParticipant>, ChatParticipant> @event)
        {
            var personalizedChat = new PersonalizedChat(@event.Chat.Name, @event.Chat.Description, @event.Chat.PrivacyMode, @event.Chat.Version, @event.Chat.ChatId, @event.Chat.Participants, @event.Chat.LastTimestamp, @event.Chat.TopIndex, 0, @event.Chat.TopIndex);
            return new ChatAddedNotification(@event.InitiatorUserId, personalizedChat);
        }

        public IChatInfoEditedNotification<ChatInfo> BuildChatInfoEditedNotification(IChatInfoEditedEvent<ChatInfo> @event)
        {
            return new ChatInfoEditedNotification(@event.InitiatorUserId, @event.ChatId, @event.ChatInfo);
        }

        public IChatRemovedNotification<ChatInfo> BuildChatRemovedNotification(IChatRemovedEvent<ChatInfo> @event)
        {
            return new ChatRemovedNotification(@event.InitiatorUserId, @event.ChatId, @event.ChatInfo);
        }
    }
}
