namespace K1vs.DotChat.Commands.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Chats;
    using DotChat.Participants;

    public class AppendChatParticipantsCommand<TParticipationCandidateCollection, TParticipationCandidate>: CommandBase, IAppendChatParticipantsCommand<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
        public AppendChatParticipantsCommand()
        {
        }

        public AppendChatParticipantsCommand(Guid initiatorUserId, Guid chatId, TParticipationCandidateCollection add, TParticipationCandidateCollection invite) : base(initiatorUserId)
        {
            ChatId = chatId;
            ToAdd = add;
            ToInvite = invite;
        }

        public Guid ChatId { get; set; }
        public TParticipationCandidateCollection ToAdd { get; set; }
        public TParticipationCandidateCollection ToInvite { get; set; }
    }
}
