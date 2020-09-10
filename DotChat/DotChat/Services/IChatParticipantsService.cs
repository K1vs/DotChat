﻿namespace K1vs.DotChat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Participants;

    public interface IChatParticipantsService
    {
        Task Add(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null);
        Task Invite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null);
        Task Apply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string style = null, string metadata = null);
        Task Remove(Guid currentUserId, Guid chatId, Guid userId);
        Task Block(Guid currentUserId, Guid chatId, Guid userId);
        Task ChangeType(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null);
        Task Append(Guid currentUserId, Guid chatId, IReadOnlyCollection<IParticipationCandidate> addCandidates, IReadOnlyCollection<IParticipationCandidate> inviteCandidates);
    }
}
