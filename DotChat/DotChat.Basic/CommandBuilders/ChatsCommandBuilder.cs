namespace K1vs.DotChat.Basic.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.CommandBuilders;
    using DotChat.Commands.Chats;
    using Models.Chats;
    using Models.Participants;
    using Participants;

    public class ChatsCommandBuilder: IChatsCommandBuilder<ChatInfo, ParticipationCandidates, List<ParticipationCandidate>, ParticipationCandidate>
    {
        public IAddChatCommand<ChatInfo, List<ParticipationCandidate>, ParticipationCandidate> BuildAddChatCommand(Guid currentUserId, ChatInfo chatInfo,
            ParticipationCandidates participationCandidates)
        {
            return new AddChatCommand<ChatInfo, List<ParticipationCandidate>, ParticipationCandidate>(currentUserId, Guid.NewGuid(), chatInfo, participationCandidates.ToAdd, participationCandidates.ToInvite);
        }

        public IEditChatInfoCommand<ChatInfo> BuildEditChatCommand(Guid currentUserId, Guid chatId, ChatInfo chatInfo)
        {
            return new EditChatInfoCommand<ChatInfo>(currentUserId, chatId, chatInfo);
        }

        public IRemoveChatCommand BuildRemoveChatCommand(Guid currentUserId, Guid chatId)
        {
            return new RemoveChatCommand(currentUserId, chatId);
        }
    }
}
