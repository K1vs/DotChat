namespace K1vs.DotChat.Common.CommandBuilders
{
    using K1vs.DotChat.CommandBuilders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Commands.Messages;
    using Messages;
    using Messages.Typed;

    public class ChatMessagesCommandBuilder: IChatMessagesCommandBuilder
    {
        public virtual IIndexChatMessageCommand BuildIndexChatMessageCommand(Guid currentUserId, Guid chatId, Guid? messageId, bool isSystem, IChatMessageInfo chatMessageInfo)
        {
            return new IndexChatMessageCommand(currentUserId, chatId, messageId ?? Guid.NewGuid(), isSystem, chatMessageInfo);
        }

        public virtual IAddChatMessageCommand BuildAddChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId, DateTime timestamp, long index, bool isSystem, IChatMessageInfo chatMessageInfo)
        {
            return new AddChatMessageCommand(currentUserId, chatId, messageId, timestamp, index, isSystem, chatMessageInfo);
        }

        public virtual IEditChatMessageCommand BuildEditChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId, IChatMessageInfo chatMessageInfo, Guid? archivedMessageId)
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
