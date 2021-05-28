namespace K1vs.DotChat.Notifications.Chats
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.PersonalizedChats;
    using Participants;

    public interface IChatAddedNotification: IChatsNotification, IHasPersonalizedChat
    {
    }
}
