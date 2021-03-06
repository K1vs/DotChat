﻿namespace K1vs.DotChat.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Chats;
    using Common.Exceptions;
    using Common.Exceptions.Access;
    using Common.Exceptions.NotFound;
    using Common.Filters;
    using Common.Paging;
    using FrameworkUtils.Extensions;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;
    using Stores.Chats;
    using Stores.Participants;

    public class ChatParticipantsPermissionValidator<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> : 
        IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate>
        where TChatsSummary : IPersonalizedChatsSummary
        where TPersonalizedChatCollection : IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatUser : IChatUser
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
        where TChatFilter : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        protected readonly IReadChatParticipantStore<TChatParticipant> ReadChatParticipantStore;
        protected readonly IReadChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> ReadChatStore;

        protected List<ChatParticipantType> ParticipantsTypesPriority = new List<ChatParticipantType>
        {
            ChatParticipantType.ReadOnlyParticipant, ChatParticipantType.MessagingOnlyParticipant,
            ChatParticipantType.Participant, ChatParticipantType.Moderator, ChatParticipantType.Admin
        };

        public ChatParticipantsPermissionValidator(IReadChatParticipantStore<TChatParticipant> readChatParticipantStore, IReadChatStore<TChatsSummary, TPersonalizedChatCollection, TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage, TChatFilter, TChatUserFilter, TMessageFilter, TPagedResult, TPagingOptions> readChatStore)
        {
            ReadChatParticipantStore = readChatParticipantStore;
            ReadChatStore = readChatStore;
        }

        protected virtual bool IsHigherParticipantType(ChatParticipantType participantType, ChatParticipantType compareToParticipantType)
        {
            var typesPriority = ParticipantsTypesPriority;
            var targetIndex = typesPriority.IndexOf(participantType);
            var compareToIndex = typesPriority.IndexOf(compareToParticipantType);
            return targetIndex > compareToIndex;
        }

        public virtual async Task ValidateAdd(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata,
            string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (CanNotAddParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.Participant),
                    serviceName, methodName, chatId, currentUserId);
            }

            if (IsHigherParticipantType(chatParticipantType, participant.ChatParticipantType))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedDueToSelfElevation, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.ParticipantType),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateInvite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata,
            string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (CanNotAddParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.Participant),
                    serviceName, methodName, chatId, currentUserId);
            }

            if (IsHigherParticipantType(chatParticipantType, participant.ChatParticipantType))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedDueToSelfElevation, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.ParticipantType),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateApply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string serviceName, string style, string metadata,
            string methodName = null)
        {
            var chat = await ReadChatStore.Retrieve(chatId);
            if(chat == null)
            {
                throw new DotChatNotFoundChatException(ErrorModule.Security, ErrorOperation.Get, chatId, currentUserId);
            }
            if (chat.PrivacyMode.NotIn(ChatPrivacyMode.Public, ChatPrivacyMode.Protected))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedDueToAdvancedProtection, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.Participant),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateRemove(Guid currentUserId, Guid chatId, Guid userId, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            var removedParticipant = await ReadChatParticipantStore.Retrieve(chatId, userId);
            if (currentUserId != userId && CanNotEditParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.Participant),
                    serviceName, methodName, chatId, currentUserId);
            }

            if (IsHigherParticipantType(removedParticipant.ChatParticipantType, participant.ChatParticipantType))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedDueToSelfElevation, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.ParticipantType),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateBlock(Guid currentUserId, Guid chatId, Guid userId, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            var removedParticipant = await ReadChatParticipantStore.Retrieve(chatId, userId);
            if (CanNotEditParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.Participant),
                    serviceName, methodName, chatId, currentUserId);
            }

            if (IsHigherParticipantType(removedParticipant.ChatParticipantType, participant.ChatParticipantType))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedDueToSelfElevation, ErrorModule.Security, ErrorOperation.Remove, ErrorEntity.ParticipantType),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateChangeType(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata,
            string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            var changedParticipant = await ReadChatParticipantStore.Retrieve(chatId, userId);
            if (CanNotEditParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Edit, ErrorEntity.Participant),
                    serviceName, methodName, chatId, currentUserId);
            }

            if (IsHigherParticipantType(changedParticipant.ChatParticipantType, participant.ChatParticipantType))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedDueToSelfElevation, ErrorModule.Security, ErrorOperation.Edit, ErrorEntity.ParticipantType),
                    serviceName, methodName, chatId, currentUserId);
            }
        }

        public virtual async Task ValidateAppend(Guid currentUserId, Guid chatId, TParticipationCandidateCollection addCandidates,
            TParticipationCandidateCollection inviteCandidates, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (CanNotAddParticipant(participant))
            {
                throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDenied, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.Participant),
                    serviceName, methodName, chatId, currentUserId);
            }

            foreach (var candidate in addCandidates.Concat(inviteCandidates))
            {
                if (IsHigherParticipantType(candidate.ChatParticipantType, participant.ChatParticipantType))
                {
                    throw new DotChatAccessDeniedException(new ErrorCode(ErrorType.AccessDeniedDueToSelfElevation, ErrorModule.Security, ErrorOperation.Add, ErrorEntity.ParticipantType),
                        serviceName, methodName, chatId, currentUserId);
                }
            }
        }

        protected virtual bool CanNotAddParticipant(TChatParticipant participant)
        {
            return participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active ||
                   participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator, ChatParticipantType.Participant);
        }

        protected virtual bool CanNotEditParticipant(TChatParticipant participant)
        {
            return participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active || 
                   participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator);
        }
    }
}
