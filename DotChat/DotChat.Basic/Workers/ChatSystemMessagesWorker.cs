namespace K1vs.DotChat.Basic.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SystemMessages;
    using Chats;
    using Configuration;
    using DotChat.CommandBuilders;
    using DotChat.SystemMessages;
    using DotChat.Workers;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Participants;

    public class ChatSystemMessagesWorker: ChatSystemMessagesWorker<ChatWorkersConfiguration, Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public ChatSystemMessagesWorker(ChatWorkersConfiguration chatWorkersConfiguration, ISystemMessagesBuilder<Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> systemMessagesBuilder, IChatMessagesCommandBuilder<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> chatMessagesCommandBuilder) : base(chatWorkersConfiguration, systemMessagesBuilder, chatMessagesCommandBuilder)
        {
        }
    }
}
