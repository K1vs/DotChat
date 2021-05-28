namespace K1vs.DotChat.Commands.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.CommandBuilders;
    using DotChat.Commands.Chats;
    using K1vs.DotChat.Chats;
    using K1vs.DotChat.Participants;
    using Participants;

    public class ChatsCommandBuilder: IChatsCommandBuilder
    {
        public virtual IAddChatCommand BuildAddChatCommand(Guid currentUserId, Guid chatId, IChatInfo chatInfo, IParticipantsAddInviteBulk participationCandidates)
        {
            return new AddChatCommand(currentUserId, chatId, chatInfo, participationCandidates);
        }

        public virtual IEditChatInfoCommand BuildEditChatCommand(Guid currentUserId, Guid chatId, IChatInfo chatInfo)
        {
            return new EditChatInfoCommand(currentUserId, chatId, chatInfo);
        }

        public virtual IRemoveChatCommand BuildRemoveChatCommand(Guid currentUserId, Guid chatId)
        {
            return new RemoveChatCommand(currentUserId, chatId);
        }
    }
}
