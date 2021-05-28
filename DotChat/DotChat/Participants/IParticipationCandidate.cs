namespace K1vs.DotChat.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common;
    using K1vs.DotChat.Users;

    public interface IParticipationCandidate: IUserRelated, ICustomizable
    {
        ChatParticipantType ChatParticipantType { get; }
    }
}
