namespace K1vs.DotChat.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Commands.Chats;
    using Participants;

    public interface IChatsCommandBuilder
    {
        IAddChatCommand BuildAddChatCommand(Guid currentUserId, Guid? chatId, IChatInfo chatInfo, IHasParticipationCandidates participationCandidates);
        IEditChatInfoCommand BuildEditChatCommand(Guid currentUserId, Guid chatId, IChatInfo chatInfo);
        IRemoveChatCommand BuildRemoveChatCommand(Guid currentUserId, Guid chatId);
    }
}
