namespace K1vs.DotChat.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Commands.Messages;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface IChatMessagesCommandBuilder
    {
        IIndexChatMessageCommand BuildIndexChatMessageCommand(Guid currentUserId, Guid chatId, Guid? messageId, bool isSystem, IChatMessageInfo chatMessageInfo);
        IAddChatMessageCommand BuildAddChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId, DateTime timestamp, long index, bool isSystem, IChatMessageInfo chatMessageInfo);
        IEditChatMessageCommand BuildEditChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId, IChatMessageInfo chatMessageInfo, Guid? archivedMessageId);
        IRemoveChatMessageCommand BuildRemoveChatMessageCommand(Guid currentUserId, Guid chatId, Guid messageId);
        IReadChatMessagesCommand BuildReadChatMessagesCommand(Guid currentUserId, Guid chatId, long index, bool force);
    }
}
