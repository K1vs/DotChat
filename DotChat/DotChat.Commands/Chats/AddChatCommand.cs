namespace K1vs.DotChat.Commands.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;

    public class AddChatCommand<TChatInfo, TParticipationCandidateCollection, TParticipationCandidate> : Command, IAddChatCommand<TChatInfo, TParticipationCandidateCollection, TParticipationCandidate>
        where TChatInfo: IChatInfo
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
        public AddChatCommand()
        {
        }

        public AddChatCommand(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo, TParticipationCandidateCollection add, TParticipationCandidateCollection invite) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
            ToAdd = add;
            ToInvite = invite;
        }

        public Guid ChatId { get; set; }
        public TChatInfo ChatInfo { get; set; }
        public TParticipationCandidateCollection ToAdd { get; set; }
        public TParticipationCandidateCollection ToInvite { get; set; }
    }
}
