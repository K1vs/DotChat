namespace K1vs.DotChat.Stores.Messages
{
    using K1vs.DotChat.Messages;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Filters;
    using Common.Paging;
    using DotChat.Chats;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public interface IChatMessageStore: IReadChatMessageStore
    {
        Task Read(Guid chatId, Guid userId, long index, bool force);
        Task<IChatMessage> Create(Guid chatId, Guid userId, Guid messageId, IChatMessageInfo messageInfo, DateTime timestamp, long index, bool isSystem, Guid creatorId);
        Task<IChatMessage> Update(Guid chatId, Guid messageId, IChatMessageInfo messageInfo, Guid modifierId);
        Task<IChatMessage> Delete(Guid chatId, Guid messageId, Guid removerId);
        Task Archive(Guid chatId, Guid originalMessageId, Guid achievedMessageId, IChatMessage messageInfo, Guid archiverId);
    }
}
