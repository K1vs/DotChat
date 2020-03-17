namespace K1vs.DotChat.Basic.CommandBuilders
{
    using K1vs.DotChat.CommandBuilders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Commands.Messages;
    using DotChat.Commands.Messages;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class ChatMessagesCommandBuilder: IChatMessagesCommandBuilder<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public virtual IIndexChatMessageCommand<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildIndexChatMessageCommand(Guid currentUserId, Guid chatId, Guid? messageId, bool isSystem, ChatMessageInfo chatMessageInfo)
        {
            return new IndexChatMessageCommand(currentUserId, chatId, messageId ?? Guid.NewGuid(), isSystem, chatMessageInfo);
        }

        public virtual IAddChatMessageCommand<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildAddChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId, DateTime timestamp, long index, bool isSystem,
            ChatMessageInfo chatMessageInfo)
        {
            return new AddChatMessageCommand(currentUserId, chatId, messageId, timestamp, index, isSystem, chatMessageInfo);
        }

        public virtual IEditChatMessageCommand<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildEditChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId,
            ChatMessageInfo chatMessageInfo, Guid? archivedMessageId)
        {
            return new EditChatMessageCommand(currentUserId, chatId, messageId, chatMessageInfo, archivedMessageId ?? Guid.NewGuid());
        }

        public virtual IRemoveChatMessageCommand BuildRemoveChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId)
        {
            return new RemoveChatMessageCommand(currentUserId, chatId, messageId);
        }

        public virtual IReadChatMessagesCommand BuildReadChatMessagesCommand(Guid currentUserId, Guid chatId, long index, bool force)
        {
            return new ReadChatMessagesCommand(currentUserId, chatId, index, force);
        }
    }
}
