namespace K1vs.DotChat.Basic.Messages.Typed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class QuoteMessage: QuoteMessage<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public QuoteMessage()
        {
        }

        public QuoteMessage(Guid chatId, Guid messageId, DateTime timestamp, ChatMessageInfo messageInfo, string style = null, string metadata = null) : base(chatId, messageId, timestamp, messageInfo, style, metadata)
        {
        }
    }
}
