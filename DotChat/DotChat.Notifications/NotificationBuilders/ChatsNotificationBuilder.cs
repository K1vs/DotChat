namespace K1vs.DotChat.Basic.NotificationBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Events.Chats;
    using DotChat.NotificationBuilders;
    using DotChat.Notifications.Chats;
    using K1vs.DotChat.PersonalizedChats;

    public class ChatsNotificationBuilder: IChatsNotificationBuilder
    {
        private readonly IPersonalizedChatBuilder _personalizedChatBuilder;

        public ChatsNotificationBuilder(IPersonalizedChatBuilder personalizedChatBuilder)
        {
            _personalizedChatBuilder = personalizedChatBuilder;
        }

        public virtual IChatAddedNotification BuildChatAddedNotification(IChatAddedEvent @event)
        {
            var personalizedChat = _personalizedChatBuilder.Build(@event.Chat, 0, 0);
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
