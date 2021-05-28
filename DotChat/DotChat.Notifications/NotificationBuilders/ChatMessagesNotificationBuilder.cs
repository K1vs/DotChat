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

    public class ChatMessagesNotificationBuilder: IChatMessagesNotificationBuilder
    {
        public virtual IChatMessageAddedNotification BuildChatMessageAddedNotification(IChatMessageAddedEvent @event)
        {
            return new ChatMessageAddedNotification(@event.InitiatorUserId, @event.ChatId, @event.Message);
        }

        public virtual IChatMessageEditedNotification BuildChatMessageEditedNotification(IChatMessageEditedEvent @event)
        {
            return new ChatMessageEditedNotification(@event.InitiatorUserId, @event.ChatId, @event.Message);
        }

        public virtual IChatMessageRemovedNotification BuildChatMessageRemovedNotification(IChatMessageRemovedEvent @event)
        {
            return new ChatMessageRemovedNotification(@event.InitiatorUserId, @event.ChatId, @event.Message);
        }
    }
}
