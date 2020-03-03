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

    public class ChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> : 
        IChatsPermissionValidator<TPersonalizedChatCollection, TPersonalizedChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions>
        where TPersonalizedChatCollection: IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TChatUser: IChatUser
        where TChatFilter: IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        private readonly IReadChatParticipantStore<TChatParticipant> _readChatParticipantStore;
        private readonly IReadUserStore<TChatUser> _readUserStore;

        public ChatsPermissionValidator(IReadChatParticipantStore<TChatParticipant> readChatParticipantStore, IReadUserStore<TChatUser> readUserStore)
        {
            _readChatParticipantStore = readChatParticipantStore;
            _readUserStore = readUserStore;
        }

        public Task ValidateGetSummary(Guid currentUserId, string serviceName, string methodName = null)
        {
            return Task.CompletedTask;
        }

        public Task ValidateGetPage(Guid currentUserId, TPagedResult chatsPage, TChatFilter filter, TPagingOptions pagingOptions,
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

        public Task ValidateGetPage(Guid currentUserId, TPagedResult chatsPage, TPagingOptions pagingOptions, string serviceName,
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

        public Task ValidateGet(Guid currentUserId, TPersonalizedChat chat, string serviceName, string methodName = null)
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

        public async Task ValidateAdd(Guid currentUserId, Guid chatId, TChatInfo chatInfo, string serviceName, string methodName = null)
        {
            var user = await _readUserStore.Retrieve(currentUserId);
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

        public async Task ValidateEditInfo(Guid currentUserId, Guid chatId, TChatInfo chatInfo, string serviceName,
            string methodName = null)
        {
            var participant = await _readChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active || participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator, ChatParticipantType.Participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Edit, ErrorEntity.ChatInfo),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public async Task ValidateRemove(Guid currentUserId, Guid chatId, string serviceName, string methodName = null)
        {
            var participant = await _readChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active || participant.ChatParticipantType.NotIn(ChatParticipantType.Admin))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.Chat),
                    serviceName, methodName, chatId, currentUserId);
            }
        }
    }
}
