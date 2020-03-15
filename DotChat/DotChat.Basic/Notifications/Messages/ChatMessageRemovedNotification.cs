namespace K1vs.DotChat.Basic.Notifications.Messages
{
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.Models.Messages.Typed;
    using K1vs.DotChat.Models.Participants;
    using K1vs.DotChat.Notifications.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ChatMessageRemovedNotification : ChatMessageRemovedNotification<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatMessageRemovedNotification()
        {
        }

        public ChatMessageRemovedNotification(Guid initiatorUserId, Guid chatId, ChatMessage message) : base(initiatorUserId, chatId, message)
        {
        }
    }
}
