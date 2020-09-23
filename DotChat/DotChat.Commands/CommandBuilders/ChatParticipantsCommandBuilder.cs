namespace K1vs.DotChat.Common.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Commands.Participants;
    using DotChat.CommandBuilders;
    using DotChat.Participants;

    public class ChatParticipantsCommandBuilder: IChatParticipantsCommandBuilder
    {
        public virtual IAddChatParticipantCommand BuildAddChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style, string metadata)
        {
            return new AddChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public virtual IApplyToChatCommand BuildApplyToChatCommand(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string style, string metadata)
        {
            return new ApplyToChatCommand(currentUserId, chatId, chatParticipantType, style, metadata);
        }

        public virtual IInviteChatParticipantCommand BuildInviteChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId,
            ChatParticipantType chatParticipantType, string style, string metadata)
        {
            return new InviteChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public virtual IRemoveChatParticipantCommand BuildRemoveChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId)
        {
            return new RemoveChatParticipantCommand(currentUserId, chatId, userId);
        }

        public virtual IChangeChatParticipantTypeCommand BuildChangeChatParticipantTypeCommand(Guid currentUserId, Guid chatId, Guid userId,
            ChatParticipantType chatParticipantType, string style, string metadata)
        {
            return new ChangeChatParticipantTypeCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public virtual IBlockChatParticipantCommand BuildBlockChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId)
        {
            return new BlockChatParticipantCommand(currentUserId, chatId, userId);
        }

        public virtual IAppendChatParticipantsCommand BuildAppendChatParticipantsCommand(Guid currentUserId, Guid chatId, IReadOnlyCollection<IParticipationCandidate> addCandidates, IReadOnlyCollection<IParticipationCandidate> inviteCandidates)
        {
            return new AppendChatParticipantsCommand(currentUserId, chatId, addCandidates, inviteCandidates);
        }
    }
}
