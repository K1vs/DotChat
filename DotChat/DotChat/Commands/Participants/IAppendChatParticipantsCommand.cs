namespace K1vs.DotChat.Commands.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IAppendChatParticipantsCommand<out TParticipationCandidateCollection, out TParticipationCandidate> : ICommandBase, IChatRelated, IHasParticipationCandidates<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
    }
}
