namespace K1vs.DotChat.Basic.Notifiers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Configuration;
    using DotChat.NotificationBuilders;
    using DotChat.Notifiers;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class ChatMessageNotifier: ChatMessageNotifier<ChatNotificationsConfiguration, ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatMessageNotifier(ChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatMessagesNotificationBuilder<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> chatMessagesNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService, chatMessagesNotificationBuilder)
        {
        }
    }
}
