namespace K1vs.DotChat.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Events.Messages;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessagesEventBuilder
    {
        IChatMessageAddedEvent BuildChatMessageAddedEvent(Guid initiatorUserId, Guid chatId, IChatMessage chatMessage);
        IChatMessageAddedEvent BuildChatMessageAddedEvent(Guid initiatorUserId, Guid chatId, Guid messageId, DateTime timestamp, long index, bool isSystem, IChatMessageInfo chatMessageInfo);
        IChatMessageEditedEvent BuildChatMessageEditedEvent(Guid initiatorUserId, Guid chatId, IChatMessage chatMessage);
        IChatMessageRemovedEvent BuildChatMessageRemovedEvent(Guid initiatorUserId, Guid chatId, IChatMessage chatMessage);
        IChatMessagesReadEvent BuildChatMessagesReadEvent(Guid initiatorUserId, Guid chatId,  long index, bool force);
    }
}
