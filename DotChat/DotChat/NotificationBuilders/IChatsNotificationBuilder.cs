namespace K1vs.DotChat.NotificationBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Events.Chats;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Notifications.Chats;
    using Participants;

    public interface IChatsNotificationBuilder
    {
        IChatAddedNotification BuildChatAddedNotification(IChatAddedEvent @event);
        IChatInfoEditedNotification BuildChatInfoEditedNotification(IChatInfoEditedEvent @event);
        IChatRemovedNotification BuildChatRemovedNotification(IChatRemovedEvent @event);
    }
}
