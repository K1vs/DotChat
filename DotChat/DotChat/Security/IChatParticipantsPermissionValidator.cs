﻿namespace K1vs.DotChat.Security
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Participants;

    public interface IChatParticipantsPermissionValidator
    {
        Task ValidateAdd(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata,  string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateInvite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateApply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string style, string metadata, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateRemove(Guid currentUserId, Guid chatId, Guid userId,  string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateBlock(Guid currentUserId, Guid chatId, Guid userId, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateChangeType(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateAppend(Guid currentUserId, Guid chatId, IReadOnlyCollection<IParticipationCandidate> addCandidates, IReadOnlyCollection<IParticipationCandidate> inviteCandidates, string serviceName, [CallerMemberName] string methodName = null);
    }
}
