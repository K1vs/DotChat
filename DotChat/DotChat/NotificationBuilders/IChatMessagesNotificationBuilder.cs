namespace K1vs.DotChat.NotificationBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Events.Messages;
    using Messages;
    using Messages.Typed;
    using Notifications.Messages;
    using Participants;

    public interface IChatMessagesNotificationBuilder
    {
        IChatMessageAddedNotification BuildChatMessageAddedNotification(IChatMessageAddedEvent @event);
        IChatMessageEditedNotification BuildChatMessageEditedNotification(IChatMessageEditedEvent @event);
        IChatMessageRemovedNotification BuildChatMessageRemovedNotification(IChatMessageRemovedEvent @event);
    }
}
