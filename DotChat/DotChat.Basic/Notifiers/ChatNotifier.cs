namespace K1vs.DotChat.Basic.Notifiers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Configuration;
    using DotChat.NotificationBuilders;
    using DotChat.Notifiers;
    using K1vs.DotChat.Basic.Messages;
    using K1vs.DotChat.Basic.Messages.Typed;
    using K1vs.DotChat.Models.Messages.Typed;
    using Models.Chats;
    using Models.Participants;

    public class ChatNotifier: ChatNotifier<ChatNotificationsConfiguration, PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatNotifier(ChatNotificationsConfiguration chatNotificationsConfiguration, INotificationSender notificationSender, INotificationRouteService notificationRouteService, IChatsNotificationBuilder<PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> chatsNotificationBuilder) : base(chatNotificationsConfiguration, notificationSender, notificationRouteService, chatsNotificationBuilder)
        {
        }
    }
}
