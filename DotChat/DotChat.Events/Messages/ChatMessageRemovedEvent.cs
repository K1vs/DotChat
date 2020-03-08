namespace K1vs.DotChat.Events.Messages
{
    using System;
    using Chats;
    using DotChat.Messages;

    public class ChatMessageRemovedEvent: Event, IChatMessageRemovedEvent
    {
        public ChatMessageRemovedEvent()
        {
        }

        public ChatMessageRemovedEvent(Guid initiatorUserId, Guid chatId, Guid messageId, long version) : base(initiatorUserId)
        {
            ChatId = chatId;
            MessageId = messageId;
            Version = version;
        }

        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }
        public long Version { get; set; }
    }
}
