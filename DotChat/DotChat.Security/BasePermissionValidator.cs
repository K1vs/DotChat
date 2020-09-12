using K1vs.DotChat.Exceptions;
using K1vs.DotChat.FrameworkUtils.Extensions;
using K1vs.DotChat.Participants;
using K1vs.DotChat.Stores.Participants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace K1vs.DotChat.Security
{
    public abstract class BasePermissionValidator
    {
        protected readonly IReadChatParticipantStore ReadChatParticipantStore;

        protected BasePermissionValidator(IReadChatParticipantStore readChatParticipantStore)
        {
            ReadChatParticipantStore = readChatParticipantStore;
        }

        protected virtual void CheckCurrentParticapantWriteAccess(IChatParticipant participant, Guid currentUserId, Guid chatId, string serviceName, string methodName)
        {
            if (participant.ChatParticipantType.NotIn(ChatParticipantType.Admin, ChatParticipantType.Moderator, ChatParticipantType.Participant, ChatParticipantType.MessagingOnlyParticipant))
            {
                throw new DotChatException(ExceptionCode.AccessDeniedNotEnoughPermissions, CreateAdditionalData(currentUserId, chatId, serviceName, methodName));
            }
        }

        protected virtual async Task<IChatParticipant> GetCurrentParticipant(Guid currentUserId, Guid chatId, string serviceName, string methodName)
        {
            var participant = await ReadChatParticipantStore.Retrieve(chatId, currentUserId);
            if (participant == null)
            {
                throw new DotChatException(ExceptionCode.AccessDeniedUndefinedPermissions, CreateAdditionalData(currentUserId, chatId, serviceName, methodName));
            }
            return participant;
        }

        protected virtual void CheckCurrentParticipantStatus(IChatParticipant participant, Guid currentUserId, Guid chatId, string serviceName, string methodName)
        {
            if (participant.ChatParticipantStatus != ChatParticipantStatus.Active)
            {
                throw new DotChatException(ExceptionCode.AccessDeniedSelfStatus, CreateAdditionalData(currentUserId, chatId, serviceName, methodName));
            }
        }

        protected virtual object CreateAdditionalData(Guid currentUserId, Guid chatId, string serviceName, string methodName)
        {
            return new
            {
                serviceName,
                methodName,
                chatId,
                currentUserId
            };
        }
    }
}
