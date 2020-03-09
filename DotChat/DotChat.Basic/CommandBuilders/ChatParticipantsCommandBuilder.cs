namespace K1vs.DotChat.Basic.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Commands.Participants;
    using DotChat.CommandBuilders;
    using DotChat.Commands.Participants;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatParticipantsCommandBuilder: IChatParticipantsCommandBuilder<List<ParticipationCandidate>, ParticipationCandidate>
    {
        public IAddChatParticipantCommand BuildAddChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId,
            ChatParticipantType chatParticipantType, string style, string metadata)
        {
            return new AddChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public IApplyToChatCommand BuildApplyToChatCommand(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string style, string metadata)
        {
            return new ApplyToChatCommand(currentUserId, chatId, chatParticipantType, style, metadata);
        }

        public IInviteChatParticipantCommand BuildInviteChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId,
            ChatParticipantType chatParticipantType, string style, string metadata)
        {
            return new InviteChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public IRemoveChatParticipantCommand BuildRemoveChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId)
        {
            return new RemoveChatParticipantCommand(currentUserId, chatId, userId);
        }

        public IChangeChatParticipantTypeCommand BuildChangeChatParticipantTypeCommand(Guid currentUserId, Guid chatId, Guid userId,
            ChatParticipantType chatParticipantType, string style, string metadata)
        {
            return new ChangeChatParticipantTypeCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public IBlockChatParticipantCommand BuildBlockChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId)
        {
            return new BlockChatParticipantCommand(currentUserId, chatId, userId);
        }

        public IAppendChatParticipantsCommand<List<ParticipationCandidate>, ParticipationCandidate> BuildAppendChatParticipantsCommand(Guid currentUserId, Guid chatId,
            List<ParticipationCandidate> addCandidates, List<ParticipationCandidate> inviteCandidates)
        {
            return new AppendChatParticipantsCommand(currentUserId, chatId, addCandidates, inviteCandidates);
        }
    }
}
