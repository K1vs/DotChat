namespace K1vs.DotChat.Security
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using FrameworkUtils.Extensions;
    using K1vs.DotChat.Exceptions;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Participants;
    using Stores.Messages;
    using Stores.Participants;

    public class ChatMessagesPermissionValidator: BasePermissionValidator, IChatMessagesPermissionValidator
    {
        protected readonly IReadChatMessageStore ReadChatMessageStore;

        public ChatMessagesPermissionValidator(IReadChatParticipantStore readChatParticipantStore, IReadChatMessageStore readChatMessageStore)
            : base(readChatParticipantStore)
        {
            ReadChatMessageStore = readChatMessageStore;
        }

        public virtual async Task ValidateGetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<IMessageFilter> filters, IPagingOptions pagingOptions, IPagedResult<IChatMessage> messagesPage, string serviceName, string methodName = null)
        {
            var participant = await GetCurrentParticipant(currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticipantStatus(participant, currentUserId, chatId, serviceName, methodName);
        }

        public async Task ValidateGetPage(Guid currentUserId, Guid chatId, IPagingOptions pagingOptions, IPagedResult<IChatMessage> messagesPage, string serviceName, [CallerMemberName] string methodName = null)
        {
            var participant = await GetCurrentParticipant(currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticipantStatus(participant, currentUserId, chatId, serviceName, methodName);
        }

        public virtual async Task ValidateRead(Guid currentUserId, Guid chatId, long index, bool force, string serviceName, string methodName = null)
        {
            var participant = await GetCurrentParticipant(currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticipantStatus(participant, currentUserId, chatId, serviceName, methodName);
        }

        public virtual async Task ValidateAdd(Guid currentUserId, Guid chatId, IChatMessageInfo messageInfo, string serviceName, string methodName = null)
        {
            var participant = await GetCurrentParticipant(currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticipantStatus(participant, currentUserId, chatId, serviceName, methodName);
        }

        public virtual async Task ValidateRemove(Guid currentUserId, Guid chatId, Guid messageId, string serviceName, string methodName = null)
        {
            var participant = await GetCurrentParticipant(currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticipantStatus(participant, currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticapantWriteAccess(participant, currentUserId, chatId, serviceName, methodName);
            var message = await ReadChatMessageStore.Retrieve(chatId, messageId);
            CheckAccessToMessage(currentUserId, participant, message, chatId, serviceName, methodName);
        }

        public virtual async Task ValidateEdit(Guid currentUserId, Guid chatId, Guid messageId, IChatMessageInfo newMessage, string serviceName, string methodName = null)
        {
            var participant = await GetCurrentParticipant(currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticipantStatus(participant, currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticapantWriteAccess(participant, currentUserId, chatId, serviceName, methodName);
            var message = await ReadChatMessageStore.Retrieve(chatId, messageId);
            CheckAccessToMessage(currentUserId, participant, message, chatId, serviceName, methodName);
        }

        protected virtual void CheckAccessToMessage(Guid currentUserId, IChatParticipant participant, IChatMessage message, Guid chatId, string serviceName, string methodName)
        {
            if (message.AuthorId != currentUserId && participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator))
            {
                throw new DotChatException(ExceptionCode.AccessDeniedNotEnoughPermissions, new {
                    serviceName,
                    methodName,
                    chatId,
                    currentUserId,
                    message.MessageId
                });
            }
        }
    }
}
