namespace K1vs.DotChat.Notifications.Messages
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using Events;

    public interface IChatMessageEditedNotification: IChatMessagesNotification, IChatRelated, IHasChatMessage
    {
    }
}
