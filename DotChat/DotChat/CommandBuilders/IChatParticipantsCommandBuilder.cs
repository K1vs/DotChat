namespace K1vs.DotChat.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using Commands.Participants;
    using Participants;

    public interface IChatParticipantsCommandBuilder
    {
        IAddChatParticipantCommand BuildAddChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata);
        IApplyToChatCommand BuildApplyToChatCommand(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string style, string metadata);
        IInviteChatParticipantCommand BuildInviteChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata);
        IRemoveChatParticipantCommand BuildRemoveChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId);
        IBlockChatParticipantCommand BuildBlockChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId);
        IChangeChatParticipantTypeCommand BuildChangeChatParticipantTypeCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata);
        IAppendChatParticipantsCommand BuildAppendChatParticipantsCommand(Guid currentUserId, Guid chatId, IReadOnlyCollection<IParticipationCandidate> addCandidates, IReadOnlyCollection<IParticipationCandidate> inviteCandidates);
    }
}
