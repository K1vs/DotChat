namespace K1vs.DotChat.Security
{
    using K1vs.DotChat.Chats;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Common;
    using Common.Exceptions;
    using Common.Exceptions.Access;
    using Common.Exceptions.NotFound;
    using Common.Filters;
    using Common.Paging;
    using FrameworkUtils.Extensions;
    using Participants;
    using Stores.Participants;
    using Stores.Users;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public class ChatsPermissionValidator: IChatsPermissionValidator
    {
        protected readonly IReadChatParticipantStore ReadChatParticipantStore;
        protected readonly IReadUserStore ReadUserStore;

        public ChatsPermissionValidator(IReadChatParticipantStore readChatParticipantStore, IReadUserStore readUserStore)
        {
            ReadChatParticipantStore = readChatParticipantStore;
            ReadUserStore = readUserStore;
        }

        public virtual Task ValidateGetSummary(Guid currentUserId, string serviceName, string methodName = null)
        {
            return Task.CompletedTask;
        }

        public virtual Task ValidateGetPage(Guid currentUserId, IPagedResult<IPersonalizedChat> chatsPage, IChatFilter filter, IPagingOptions pagingOptions,
            string serviceName, string methodName = null)
        {
            var noAccess = chatsPage.Items.Where(chat => chat.PrivacyMode.NotIn(ChatPrivacyMode.Public, ChatPrivacyMode.Protected))
                .FirstOrDefault(chat =>
                {
                    var participant = chat.Participants.FirstOrDefault(r => r.UserId == currentUserId);
                    return participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active;
                });
            if (noAccess != null)
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Get, ErrorEntity.PersonalizedChat),
                    serviceName, methodName, noAccess.ChatId, currentUserId);
            }
            return Task.CompletedTask;
        }

        public virtual Task ValidateGetPage(Guid currentUserId, IPagedResult<IPersonalizedChat> chatsPage, IPagingOptions pagingOptions, string serviceName,
            string methodName = null)
        {
            var noAccess = chatsPage.Items.Where(chat => chat.PrivacyMode.NotIn(ChatPrivacyMode.Public, ChatPrivacyMode.Protected))
                .FirstOrDefault(chat =>
                {
                    var participant = chat.Participants.FirstOrDefault(r => r.UserId == currentUserId);
                    return participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active;
                });
            if (noAccess != null)
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Get, ErrorEntity.PersonalizedChat),
                    serviceName, methodName, noAccess.ChatId, currentUserId);
            }
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
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Get, ErrorEntity.PersonalizedChat),
                    serviceName, methodName, chat.ChatId, currentUserId);
            }
            return Task.CompletedTask;
        }

        public virtual async Task ValidateAdd(Guid currentUserId, Guid chatId, IChatInfo chatInfo, string serviceName, string methodName = null)
        {
            var user = await ReadUserStore.Retrieve(currentUserId);
            if (user == null)
            {
                throw new DotChatNotFoundUserException(ErrorModule.Security, ErrorOperation.Add, chatId, currentUserId);
            }
            if (!user.CanCreateChat)
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.Chat),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateEditInfo(Guid currentUserId, Guid chatId, IChatInfo chatInfo, string serviceName,
            string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active || participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator, ChatParticipantType.Participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Edit, ErrorEntity.ChatInfo),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateRemove(Guid currentUserId, Guid chatId, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active || participant.ChatParticipantType.NotIn(ChatParticipantType.Admin))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.Chat),
                    serviceName, methodName, chatId, currentUserId);
            }
        }
    }
}
