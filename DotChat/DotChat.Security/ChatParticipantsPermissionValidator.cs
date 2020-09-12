namespace K1vs.DotChat.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using FrameworkUtils.Extensions;
    using K1vs.DotChat.Exceptions;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;
    using Stores.Chats;
    using Stores.Participants;

    public class ChatParticipantsPermissionValidator: BasePermissionValidator, IChatParticipantsPermissionValidator
    {
        protected readonly IReadChatStore ReadChatStore;

        protected List<ChatParticipantType> ParticipantsTypesPriority = new List<ChatParticipantType>
        {
            ChatParticipantType.ReadOnlyParticipant, ChatParticipantType.MessagingOnlyParticipant,
            ChatParticipantType.Participant, ChatParticipantType.Moderator, ChatParticipantType.Admin
        };

        public ChatParticipantsPermissionValidator(IReadChatParticipantStore readChatParticipantStore, IReadChatStore readChatStore)
            : base(readChatParticipantStore)
        {
            ReadChatStore = readChatStore;
        }

        public virtual async Task ValidateAdd(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata,
            string serviceName, string methodName = null)
        {
            var participant = await GetCurrentParticipant(currentUserId, chatId, serviceName, methodName);
            CheckAddParticipant(participant, currentUserId, chatId, serviceName, methodName);
            CheckHigherParticipantType(participant, chatParticipantType, currentUserId, chatId, serviceName, methodName);
        }

        public virtual async Task ValidateInvite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            CheckAddParticipant(participant, currentUserId, chatId, serviceName, methodName);
            CheckHigherParticipantType(participant, chatParticipantType, currentUserId, chatId, serviceName, methodName);
        }

        public virtual async Task ValidateApply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string serviceName, string style, string metadata, string methodName = null)
        {
            var chat = await ReadChatStore.Retrieve(chatId);
            if(chat == null)
            {
                throw new DotChatException(ExceptionCode.StorageFaultItemNotFound, new
                {
                    serviceName,
                    methodName,
                    chatId,
                    currentUserId,
                    chatParticipantType
                });
            }
            if (chat.PrivacyMode.NotIn(ChatPrivacyMode.Public, ChatPrivacyMode.Protected))
            {
                throw new DotChatException(ExceptionCode.AccessDeniedDueToAdvancedProtection, new
                {
                    serviceName,
                    methodName,
                    chatId,
                    currentUserId,
                    chatParticipantType
                });
            }
        }

        public virtual async Task ValidateRemove(Guid currentUserId, Guid chatId, Guid userId, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            var removedParticipant = await ReadChatParticipantStore.Retrieve(chatId, userId);
            CheckAddParticipant(participant, currentUserId, chatId, serviceName, methodName);
            CheckHigherParticipantType(participant, removedParticipant.ChatParticipantType, currentUserId, chatId, serviceName, methodName);
        }

        public virtual async Task ValidateBlock(Guid currentUserId, Guid chatId, Guid userId, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            var removedParticipant = await ReadChatParticipantStore.Retrieve(chatId, userId);
            CheckAddParticipant(participant, currentUserId, chatId, serviceName, methodName);
            CheckHigherParticipantType(participant, removedParticipant.ChatParticipantType, currentUserId, chatId, serviceName, methodName);
        }

        public virtual async Task ValidateChangeType(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata,
            string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            var changedParticipant = await ReadChatParticipantStore.Retrieve(chatId, userId);
            CheckAddParticipant(participant, currentUserId, chatId, serviceName, methodName);
            CheckHigherParticipantType(participant, changedParticipant.ChatParticipantType, currentUserId, chatId, serviceName, methodName);
        }

        public virtual async Task ValidateAppend(Guid currentUserId, Guid chatId, IReadOnlyCollection<IParticipationCandidate> addCandidates, IReadOnlyCollection<IParticipationCandidate> inviteCandidates, string serviceName, string methodName = null)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            CheckAddParticipant(participant, currentUserId, chatId, serviceName, methodName);

            foreach (var candidate in addCandidates.Concat(inviteCandidates))
            {
                CheckHigherParticipantType(participant, candidate.ChatParticipantType, currentUserId, chatId, serviceName, methodName);
            }
        }

        protected virtual void CheckAddParticipant(IChatParticipant participant, Guid currentUserId, Guid chatId, string serviceName, string methodName)
        {
            if(participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator, ChatParticipantType.Participant))
            {
                throw new DotChatException(ExceptionCode.AccessDeniedNotEnoughPermissions, CreateAdditionalData(currentUserId, chatId, serviceName, methodName));
            }
        }

        protected virtual void CheckEditParticipant(IChatParticipant participant, Guid currentUserId, Guid chatId, string serviceName, string methodName)
        {
            if (participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator))
            {
                throw new DotChatException(ExceptionCode.AccessDeniedNotEnoughPermissions, CreateAdditionalData(currentUserId, chatId, serviceName, methodName));
            }
        }

        protected virtual void CheckHigherParticipantType(IChatParticipant participant, ChatParticipantType chatParticipantType, Guid currentUserId, Guid chatId, string serviceName, string methodName)
        {
            if (IsHigherParticipantType(chatParticipantType, participant.ChatParticipantType))
            {
                throw new DotChatException(ExceptionCode.AccessDeniedDueToSelfElevation, new {
                    serviceName,
                    methodName,
                    chatId,
                    currentUserId,
                    chatParticipantType
                });
            }
        }

        protected override async Task<IChatParticipant> GetCurrentParticipant(Guid currentUserId, Guid chatId, string serviceName, string methodName)
        {
            var participant = await base.GetCurrentParticipant(currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticipantStatus(participant, currentUserId, chatId, serviceName, methodName);
            CheckCurrentParticapantWriteAccess(participant, currentUserId, chatId, serviceName, methodName);
            return participant;
        }

        protected virtual bool IsHigherParticipantType(ChatParticipantType participantType, ChatParticipantType compareToParticipantType)
        {
            var typesPriority = ParticipantsTypesPriority;
            var targetIndex = typesPriority.IndexOf(participantType);
            var compareToIndex = typesPriority.IndexOf(compareToParticipantType);
            return targetIndex > compareToIndex;
        }
    }
}
