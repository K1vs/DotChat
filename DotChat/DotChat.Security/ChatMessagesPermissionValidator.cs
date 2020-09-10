namespace K1vs.DotChat.Security
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Chats;
    using Common;
    using Common.Exceptions;
    using Common.Exceptions.Access;
    using Common.Filters;
    using Common.Paging;
    using FrameworkUtils.Extensions;
    using Messages;
    using Messages.Typed;
    using Participants;
    using Stores.Messages;
    using Stores.Participants;

    public class ChatMessagesPermissionValidator: IChatMessagesPermissionValidator
    {
        protected readonly IReadChatParticipantStore ReadChatParticipantStore;
        protected readonly IReadChatMessageStore ReadChatMessageStore;

        public ChatMessagesPermissionValidator(IReadChatParticipantStore readChatParticipantStore, IReadChatMessageStore readChatMessageStore)
        {
            ReadChatParticipantStore = readChatParticipantStore;
            ReadChatMessageStore = readChatMessageStore;
        }

        public virtual async Task ValidateGetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<IMessageFilter> filters, IPagingOptions pagingOptions, IPagedResult<IChatMessage> messagesPage, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active)
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Get, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public async Task ValidateGetPage(Guid currentUserId, Guid chatId, IPagingOptions pagingOptions, IPagedResult<IChatMessage> messagesPage, string serviceName, [CallerMemberName] string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active)
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Get, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateRead(Guid currentUserId, Guid chatId, long index, bool force, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active)
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Mark, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateAdd(Guid currentUserId, Guid chatId, IChatMessageInfo messageInfo, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (IsBadParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateRemove(Guid currentUserId, Guid chatId, Guid messageId, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (IsBadParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
            var message = await ReadChatMessageStore.Retrieve(chatId, messageId);
            if (message.AuthorId != currentUserId && participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedForeign, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateEdit(Guid currentUserId, Guid chatId, Guid messageId, IChatMessageInfo newMessage, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (IsBadParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Edit, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
            var message = await ReadChatMessageStore.Retrieve(chatId, messageId);
            if (message.AuthorId != currentUserId && participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedForeign, ErrorModule.Security, ErrorOperation.Edit, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        protected virtual bool IsBadParticipant(IChatParticipant participant)
        {
            return participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active ||
                   participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator, ChatParticipantType.Participant, ChatParticipantType.MessagingOnlyParticipant);
        }
    }
}
