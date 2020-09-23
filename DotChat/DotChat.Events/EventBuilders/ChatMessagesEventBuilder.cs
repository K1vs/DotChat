namespace K1vs.DotChat.Basic.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.EventBuilders;
    using DotChat.Events.Messages;
    using DotChat.Messages;
    using K1vs.DotChat.Models.Messages;

    public class ChatMessagesEventBuilder: IChatMessagesEventBuilder
    {
        public virtual IChatMessageAddedEvent BuildChatMessageAddedEvent(Guid initiatorUserId, Guid chatId, IChatMessage chatMessage)
        {
            return new ChatMessageAddedEvent(initiatorUserId, chatId, chatMessage);
        }

        public virtual IChatMessageAddedEvent BuildChatMessageAddedEvent(Guid initiatorUserId, Guid chatId, Guid messageId, DateTime timestamp, long index, bool isSystem, IChatMessageInfo chatMessageInfo)
        {
            var message = new ChatMessage(messageId, timestamp, index, initiatorUserId, MessageStatus.Actual, null, isSystem, chatMessageInfo);
            return new ChatMessageAddedEvent(initiatorUserId, chatId, message);
        }

        public virtual IChatMessageEditedEvent BuildChatMessageEditedEvent(Guid initiatorUserId, Guid chatId, IChatMessage chatMessage)
        {
            return new ChatMessageEditedEvent(initiatorUserId, chatId, chatMessage);
        }

        public virtual IChatMessageRemovedEvent BuildChatMessageRemovedEvent(Guid initiatorUserId, Guid chatId, IChatMessage chatMessage)
        {
            return new ChatMessageRemovedEvent(initiatorUserId, chatId, chatMessage);
        }

        public virtual IChatMessagesReadEvent BuildChatMessagesReadEvent(Guid initiatorUserId, Guid chatId, long index, bool force)
        {
            return new ChatMessagesReadEvent(initiatorUserId, chatId, index, force);
        }
    }
}
