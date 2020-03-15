namespace K1vs.DotChat.Basic.Events.Messages
{
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Events.Messages;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.Models.Messages.Typed;
    using K1vs.DotChat.Models.Participants;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ChatMessageRemovedEvent : ChatMessageRemovedEvent<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatMessageRemovedEvent()
        {
        }

        public ChatMessageRemovedEvent(Guid initiatorUserId, Guid chatId, ChatMessage message) : base(initiatorUserId, chatId, message)
        {
        }
    }
}
