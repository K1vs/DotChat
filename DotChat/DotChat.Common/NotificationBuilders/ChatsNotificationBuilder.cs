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
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Chats;
    using Models.Participants;

    public class ChatsNotificationBuilder: IChatsNotificationBuilder
    {
        public virtual IChatAddedNotification BuildChatAddedNotification(IChatAddedEvent @event)
        {
            var personalizedChat = new PersonalizedChat(@event.Chat.Name, @event.Chat.Description, @event.Chat.PrivacyMode, @event.Chat.Version, @event.Chat.ChatId, @event.Chat.Participants, @event.Chat.LastTimestamp, @event.Chat.TopIndex, @event.Chat.LastMessageId, @event.Chat.LastMessageAuthorId, @event.Chat.LastChatMessageInfo, 0, @event.Chat.TopIndex);
            return new ChatAddedNotification(@event.InitiatorUserId, personalizedChat);
        }

        public virtual IChatInfoEditedNotification BuildChatInfoEditedNotification(IChatInfoEditedEvent @event)
        {
            return new ChatInfoEditedNotification(@event.InitiatorUserId, @event.ChatId, @event.ChatInfo);
        }

        public virtual IChatRemovedNotification BuildChatRemovedNotification(IChatRemovedEvent @event)
        {
            return new ChatRemovedNotification(@event.InitiatorUserId, @event.ChatId, @event.ChatInfo);
        }
    }
}
