namespace K1vs.DotChat.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Commands.Chats;
    using Participants;

    public interface IChatsCommandBuilder<TChatInfo, in TParticipationCandidates, out TParticipationCandidateCollection, out TParticipationCandidate>
        where TChatInfo : IChatInfo
        where TParticipationCandidates: IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
        IAddChatCommand<TChatInfo, TParticipationCandidateCollection, TParticipationCandidate> BuildAddChatCommand(Guid currentUserId, TChatInfo chatInfo, TParticipationCandidates participationCandidates);
        IEditChatInfoCommand<TChatInfo> BuildEditChatCommand(Guid currentUserId, Guid chatId, TChatInfo chatInfo);
        IRemoveChatCommand BuildRemoveChatCommand(Guid currentUserId, Guid chatId);
    }
}
