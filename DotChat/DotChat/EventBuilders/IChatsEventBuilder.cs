namespace K1vs.DotChat.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Events.Chats;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IChatsEventBuilder
    {
        IChatAddedEvent BuildChatAddedEvent(Guid initiatorUserId, IChat chat);
        IChatInfoEditedEvent BuildChatInfoEditedEvent(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo);
        IChatRemovedEvent BuildChatRemovedEvent(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo);
    }
}
