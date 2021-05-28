namespace K1vs.DotChat.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using Commands.Participants;
    using Participants;

    public interface IChatParticipantsCommandBuilder
    {
        IAddChatParticipantCommand BuildAddChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null);
        IApplyToChatCommand BuildApplyToChatCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null);
        IInviteChatParticipantCommand BuildInviteChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null);
        IRemoveChatParticipantCommand BuildRemoveChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId);
        IBlockChatParticipantCommand BuildBlockChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId);
        IChangeChatParticipantTypeCommand BuildChangeChatParticipantTypeCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null);
        IBulkAddInviteChatParticipantsCommand BuildBulkAddInviteChatParticipantsCommand(Guid currentUserId, Guid chatId, IParticipantsAddInviteBulk participantsAddInviteBulk);
        IReadChatParticipantCommand BuildReadChatParticipantCommand(Guid currentUserId, Guid chatId, long index, bool force);
    }
}
