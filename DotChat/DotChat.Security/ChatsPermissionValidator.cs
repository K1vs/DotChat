namespace K1vs.DotChat.Security
{
    using K1vs.DotChat.Chats;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using FrameworkUtils.Extensions;
    using Participants;
    using Stores.Participants;
    using Stores.Users;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Exceptions;

    public class ChatsPermissionValidator: BasePermissionValidator, IChatsPermissionValidator
    {
        protected readonly IReadUserStore ReadUserStore;

        public ChatsPermissionValidator(IReadChatParticipantStore readChatParticipantStore, IReadUserStore readUserStore)
            : base(readChatParticipantStore)
        {
            ReadUserStore = readUserStore;
        }

        public virtual Task ValidateGetSummary(Guid currentUserId, string serviceName, string methodName = null)
        {
            return Task.CompletedTask;
        }

        public virtual Task ValidateGetPage(Guid currentUserId, IPagedResult<IPersonalizedChat> chatsPage, IChatFilter filter, IPagingOptions pagingOptions,
            string serviceName, string methodName = null)
        {
            CheckCanGetPage(currentUserId, chatsPage, serviceName, methodName);
            return Task.CompletedTask;
        }

        public virtual Task ValidateGetPage(Guid currentUserId, IPagedResult<IPersonalizedChat> chatsPage, IPagingOptions pagingOptions, string serviceName,
            string methodName = null)
        {
            CheckCanGetPage(currentUserId, chatsPage, serviceName, methodName);
            return Task.CompletedTask;
        }

        public virtual Task ValidateGet(Guid currentUserId, IPersonalizedChat chat, string serviceName, string methodName = null)
        {
            if (chat.PrivacyMode.In(ChatPrivacyMode.Public, ChatPrivacyMode.Protected))
            {
                return Task.CompletedTask;
            }
            var participant = chat.Participants.FirstOrDefault(r => r.UserId == currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active)
            {
                throw new DotChatException(ExceptionCode.AccessDeniedForeign, new
                {
                    serviceName,
                    methodName,
                    chat.ChatId,
                    currentUserId
                });
            }
            return Task.CompletedTask;
        }

        public virtual async Task ValidateAdd(Guid currentUserId, Guid chatId, IChatInfo chatInfo, string serviceName, string methodName = null)
        {
            var user = await ReadUserStore.Retrieve(currentUserId);
            if (user == null)
            {
                throw new DotChatException(ExceptionCode.StorageFaultItemNotFound, new
                {
                    serviceName,
                    methodName,
                    chatId,
                    currentUserId
                });
            }
            if (!user.CanCreateChat)
            {
                throw new DotChatException(ExceptionCode.AccessDeniedNotEnoughPermissions, new
                {
                    serviceName,
                    methodName,
                    chatId,
                    currentUserId
                });
            }
        }

        public virtual async Task ValidateEditInfo(Guid currentUserId, Guid chatId, IChatInfo chatInfo, string serviceName,
            string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active || participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator, ChatParticipantType.Participant))
            {
                throw new DotChatException(ExceptionCode.AccessDeniedForeign, new
                {
                    serviceName,
                    methodName,
                    chatId,
                    currentUserId
                });
            }
        }

        public virtual async Task ValidateRemove(Guid currentUserId, Guid chatId, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active || participant.ChatParticipantType.NotIn(ChatParticipantType.Admin))
            {
                throw new DotChatException(ExceptionCode.AccessDeniedForeign, new
                {
                    serviceName,
                    methodName,
                    chatId,
                    currentUserId
                });
            }
        }

        protected virtual void CheckCanGetPage(Guid currentUserId, IPagedResult<IPersonalizedChat> chatsPage, string serviceName, string methodName)
        {
            var noAccess = chatsPage.Items.Where(chat => chat.PrivacyMode.NotIn(ChatPrivacyMode.Public, ChatPrivacyMode.Protected))
                .FirstOrDefault(chat =>
                {
                    var participant = chat.Participants.FirstOrDefault(r => r.UserId == currentUserId);
                    return participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active;
                });
            if (noAccess != null)
            {
                throw new DotChatException(ExceptionCode.AccessDeniedForeign, new
                {
                    serviceName,
                    methodName,
                    currentUserId
                });
            }
        }
    }
}
