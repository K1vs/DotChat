namespace K1vs.DotChat.Notifications.Messages
{
    using DotChat.Chats;
    using DotChat.Messages;
    using Events;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;
    using System.Collections.Generic;

    public interface IChatMessageRemovedNotification: IChatMessagesNotification, IChatRelated, IHasChatMessage
    {
    }
}
