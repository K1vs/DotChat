namespace K1vs.DotChat.Basic.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.CommandBuilders;
    using DotChat.Commands.Chats;
    using K1vs.DotChat.Basic.Commands.Chats;
    using Models.Chats;
    using Models.Participants;
    using Participants;

    public class ChatsCommandBuilder: IChatsCommandBuilder<ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate>
    {
        public virtual IAddChatCommand<ChatInfo, List<ParticipationCandidate>, ParticipationCandidate> BuildAddChatCommand(Guid currentUserId, Guid? chatId, ChatInfo chatInfo,
            ParticipationCandidates participationCandidates)
        {
            return new AddChatCommand(currentUserId, chatId ?? Guid.NewGuid(), chatInfo, participationCandidates.ToAdd, participationCandidates.ToInvite);
        }

        public virtual IEditChatInfoCommand<ChatInfo> BuildEditChatCommand(Guid currentUserId, Guid chatId, ChatInfo chatInfo)
        {
            return new EditChatInfoCommand(currentUserId, chatId, chatInfo);
        }

        public virtual IRemoveChatCommand BuildRemoveChatCommand(Guid currentUserId, Guid chatId)
        {
            return new RemoveChatCommand(currentUserId, chatId);
        }
    }
}
