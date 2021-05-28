namespace K1vs.DotChat.Commands.CommandBuilders
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
        public virtual IAddChatParticipantCommand BuildAddChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null)
        {
            return new AddChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, styles);
        }

        public virtual IApplyToChatCommand BuildApplyToChatCommand(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null)
        {
            return new ApplyToChatCommand(currentUserId, chatId, userId, chatParticipantType, styles);
        }

        public virtual IInviteChatParticipantCommand BuildInviteChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId,
            ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null)
        {
            return new InviteChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, styles);
        }

        public virtual IRemoveChatParticipantCommand BuildRemoveChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId)
        {
            return new RemoveChatParticipantCommand(currentUserId, chatId, userId);
        }

        public virtual IChangeChatParticipantTypeCommand BuildChangeChatParticipantTypeCommand(Guid currentUserId, Guid chatId, Guid userId,
            ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null)
        {
            return new ChangeChatParticipantTypeCommand(currentUserId, chatId, userId, chatParticipantType, styles);
        }

        public virtual IBlockChatParticipantCommand BuildBlockChatParticipantCommand(Guid currentUserId, Guid chatId, Guid userId)
        {
            return new BlockChatParticipantCommand(currentUserId, chatId, userId);
        }

        public virtual IBulkAddInviteChatParticipantsCommand BuildBulkAddInviteChatParticipantsCommand(Guid currentUserId, Guid chatId, IParticipantsAddInviteBulk participantsAddInviteBulk)
        {
            return new BulkAddInviteChatParticipantsCommand(currentUserId, chatId, participantsAddInviteBulk);
        }

        public virtual IReadChatParticipantCommand BuildReadChatParticipantCommand(Guid currentUserId, Guid chatId, long index, bool force)
        {
            return new ReadChatMessagesCommand(currentUserId, chatId, index, force);
        }
    }
}
