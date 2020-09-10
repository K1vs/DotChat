namespace K1vs.DotChat.Security
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

    public class ChatParticipantsPermissionValidator: IChatParticipantsPermissionValidator
    {
        protected readonly IReadChatParticipantStore ReadChatParticipantStore;
        protected readonly IReadChatStore ReadChatStore;

        protected List<ChatParticipantType> ParticipantsTypesPriority = new List<ChatParticipantType>
        {
            ChatParticipantType.ReadOnlyParticipant, ChatParticipantType.MessagingOnlyParticipant,
            ChatParticipantType.Participant, ChatParticipantType.Moderator, ChatParticipantType.Admin
        };

        public ChatParticipantsPermissionValidator(IReadChatParticipantStore readChatParticipantStore, IReadChatStore readChatStore)
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

        public virtual async Task ValidateInvite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata, string serviceName, string methodName = null)
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

        public virtual async Task ValidateApply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string serviceName, string style, string metadata, string methodName = null)
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

        public virtual async Task ValidateAppend(Guid currentUserId, Guid chatId, IReadOnlyCollection<IParticipationCandidate> addCandidates, IReadOnlyCollection<IParticipationCandidate> inviteCandidates, string serviceName, string methodName = null)
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

        protected virtual bool CanNotAddParticipant(IChatParticipant participant)
        {
            return participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active ||
                   participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator, ChatParticipantType.Participant);
        }

        protected virtual bool CanNotEditParticipant(IChatParticipant participant)
        {
            return participant == null || participant.ChatParticipantStatus != ChatParticipantStatus.Active || 
                   participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator);
        }
    }
}
