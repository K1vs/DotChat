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

    public class ChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatParticipant, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> : 
        IChatMessagesPermissionValidator<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, 
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions>
        where TChatInfo : IChatInfo
        where TChatParticipant : IChatParticipant
        where TChatUser : IChatUser
        where TChatMessageCollection : IReadOnlyCollection<TChatMessage>
        where TChatMessage : IChatMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TChatMessageCollection, TChatMessage>
        where TPagingOptions : IPagingOptions
    {
        private readonly IReadChatParticipantStore<TChatParticipant> _readChatParticipantStore;
        private readonly IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> _readChatMessageStore;

        public ChatMessagesPermissionValidator(IReadChatParticipantStore<TChatParticipant> readChatParticipantStore, IReadChatMessageStore<TChatInfo, TChatUser, TChatMessageCollection, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TMessageFilter, TPagedResult, TPagingOptions> readChatMessageStore)
        {
            _readChatParticipantStore = readChatParticipantStore;
            _readChatMessageStore = readChatMessageStore;
        }

        public virtual async Task ValidateGetPage(Guid currentUserId, Guid chatId, IReadOnlyCollection<TMessageFilter> filters, TPagingOptions pagingOptions,
            TPagedResult messagesPage, string serviceName, string methodName = null)
        {
            var participant = await _readChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active)
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Get, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public async Task ValidateGetPage(Guid currentUserId, Guid chatId, TPagingOptions pagingOptions, TPagedResult messagesPage, string serviceName, [CallerMemberName] string methodName = null)
        {
            var participant = await _readChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active)
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Get, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateRead(Guid currentUserId, Guid chatId, long index, string serviceName, string methodName = null)
        {
            var participant = await _readChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active)
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Mark, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateAdd(Guid currentUserId, Guid chatId, TChatMessageInfo messageInfo, string serviceName,
            string methodName = null)
        {
            var participant = await _readChatParticipantStore.Retrieve(chatId, currentUserId);
            if (IsBadParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateRemove(Guid currentUserId, Guid chatId, Guid messageId, string serviceName, string methodName = null)
        {
            var participant = await _readChatParticipantStore.Retrieve(chatId, currentUserId);
            if (IsBadParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
            var message = await _readChatMessageStore.Retrieve(chatId, messageId);
            if (message.AuthorId != currentUserId && participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedForeign, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateEdit(Guid currentUserId, Guid chatId, Guid messageId, TChatMessageInfo newMessage, string serviceName,
            string methodName = null)
        {
            var participant = await _readChatParticipantStore.Retrieve(chatId, currentUserId);
            if (IsBadParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Edit, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
            var message = await _readChatMessageStore.Retrieve(chatId, messageId);
            if (message.AuthorId != currentUserId && participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedForeign, ErrorModule.Security, ErrorOperation.Edit, ErrorEntity.Message),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        protected virtual bool IsBadParticipant(TChatParticipant participant)
        {
            return participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active ||
                   participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator, ChatParticipantType.Participant, ChatParticipantType.MessagingOnlyParticipant);
        }
    }
}
