namespace K1vs.DotChat.Events.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IChatParticipantBulkAddedInvitedEvent: IChatParticipantEvent, IChatRelated
    {
        IReadOnlyCollection<IParticipationModificationResult> Added { get; }
        IReadOnlyCollection<IParticipationModificationResult> Invited { get; }
    }
}
