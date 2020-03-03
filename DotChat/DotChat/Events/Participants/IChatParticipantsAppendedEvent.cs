namespace K1vs.DotChat.Events.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Participants;

    public interface IChatParticipantsAppendedEvent<out TParticipationResultCollection, out TParticipationResult, out TChatParticipant> : IEventBase, IChatRelated
        where TParticipationResultCollection: IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        TParticipationResultCollection Added { get; }
        TParticipationResultCollection Invited { get; }
    }
}
