namespace K1vs.DotChat.Models.Messages.Typed
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using Participants;

    public class QuoteMessage: IQuoteMessage
    {
        public QuoteMessage()
        {
        }

        public QuoteMessage(Guid chatId, Guid messageId, DateTime timestamp, IChatMessageInfo messageInfo, IReadOnlyList<string> styles = null)
        {
            ChatId = chatId;
            MessageId = messageId;
            Timestamp = timestamp;
            MessageInfo = messageInfo;
            Styles = styles;
        }

        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }
        public DateTime Timestamp { get; set; }
        public IChatMessageInfo MessageInfo { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}
