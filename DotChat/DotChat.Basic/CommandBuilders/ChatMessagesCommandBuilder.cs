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
        public IIndexChatMessageCommand<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildIndexChatMessageCommand(Guid currentUserId, Guid chatId, Guid? messageId, ChatMessageInfo chatMessageInfo)
        {
            return new IndexChatMessageCommand(currentUserId, chatId, messageId ?? Guid.NewGuid(), chatMessageInfo);
        }

        public IAddChatMessageCommand<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildAddChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId, long index,
            ChatMessageInfo chatMessageInfo)
        {
            return new AddChatMessageCommand(currentUserId, chatId, messageId, index, chatMessageInfo);
        }

        public IEditChatMessageCommand<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> BuildEditChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId,
            ChatMessageInfo chatMessageInfo, Guid? archivedMessageId)
        {
            return new EditChatMessageCommand(currentUserId, chatId, messageId, chatMessageInfo, archivedMessageId ?? Guid.NewGuid());
        }

        public IRemoveChatMessageCommand BuildRemoveChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId)
        {
            return new RemoveChatMessageCommand(currentUserId, chatId, messageId);
        }

        public IReadChatMessagesCommand BuildReadChatMessagesCommand(Guid currentUserId, Guid chatId, long index)
        {
            return new ReadChatMessagesCommand(currentUserId, chatId, index);
        }
    }
}
