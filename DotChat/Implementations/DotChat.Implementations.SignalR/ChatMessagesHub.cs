namespace K1vs.DotChat.Implementations.SignalR
{
    using K1vs.DotChat.Chats;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Configuration;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;
    using K1vs.DotChat.Services;
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class ChatMessagesHub<TChatMessagesClient, TMessageFilterCollection, TPagingOptions, TChatMessageInfo>
        : Hub<TChatMessagesClient>
        where TChatMessagesClient: class, IChatMessagesClient
        where TMessageFilterCollection: IReadOnlyCollection<IMessageFilter>
        where TPagingOptions: IPagingOptions
        where TChatMessageInfo: IChatMessageInfo
    {
        protected readonly IChatMessagesService ChatMessagesService;

        public ChatMessagesHub(IChatMessagesService chatMessagesService)
        {
            ChatMessagesService = chatMessagesService;
        }

        public virtual async Task<IPagedResult<IChatMessage>> GetPage(Guid chatId, TMessageFilterCollection filters, TPagingOptions pagingOptions)
        {
            return await ChatMessagesService.GetPage(CurrentUserId, chatId, filters, pagingOptions);
        }

        public virtual async Task<IPagedResult<IChatMessage>> GetPage(Guid chatId, TPagingOptions pagingOptions)
        {
            return await ChatMessagesService.GetPage(CurrentUserId, chatId, pagingOptions);
        }

        public virtual async Task<IPagedResult<IChatMessage>> GetPage(Guid chatId)
        {
            return await ChatMessagesService.GetPage(CurrentUserId, chatId);
        }

        public virtual async Task Read(Guid chatId, long index, bool force)
        {
            await ChatMessagesService.Read(CurrentUserId, chatId, index, force);
        }

        public virtual async Task<Guid> Add(Guid chatId, Guid? messageId, TChatMessageInfo messageInfo)
        {
            return await ChatMessagesService.Add(CurrentUserId, chatId, messageId, messageInfo);
        }

        public virtual async Task<Guid> Edit(Guid chatId, Guid messageId, TChatMessageInfo messageInfo, Guid? archivedMessageId)
        {
            return await ChatMessagesService.Edit(CurrentUserId, chatId, messageId, messageInfo, archivedMessageId);
        }

        public virtual async Task Remove(Guid chatId, Guid messageId)
        {
            await ChatMessagesService.Remove(CurrentUserId, chatId, messageId);
        }

        protected virtual Guid CurrentUserId => Guid.Parse(Context.User.Identity.Name);
    }
}
