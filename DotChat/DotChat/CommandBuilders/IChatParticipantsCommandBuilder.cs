namespace K1vs.DotChat.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using Commands.Participants;
    using Participants;

    public interface IChatParticipantsCommandBuilder<TParticipationCandidateCollection, out TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
        IAddChatParticipantCommand BuildAddChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata);
        IApplyToChatCommand BuildApplyToChatCommand(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string style, string metadata);
        IInviteChatParticipantCommand BuildInviteChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata);
        IRemoveChatParticipantCommand BuildRemoveChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType);
        IChangeChatParticipantTypeCommand BuildChangeChatParticipantTypeCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata);
        IBlockChatParticipantCommand BuildBlockChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType);
        IAppendChatParticipantsCommand<TParticipationCandidateCollection, TParticipationCandidate> BuildAppendChatParticipantCommand(Guid currentUserId, Guid chatId, TParticipationCandidateCollection addCandidates,
            TParticipationCandidateCollection inviteCandidates);
    }
}
