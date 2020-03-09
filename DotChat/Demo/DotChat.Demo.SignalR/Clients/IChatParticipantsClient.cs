namespace K1vs.DotChat.Demo.SignalR.Clients
{
    using System.Collections.Generic;
    using Basic.Participants;
    using Implementations.SignalR;
    using Models.Participants;

    public interface IChatParticipantsClient: IChatParticipantsClient<List<ParticipationResult>, ParticipationResult, ChatParticipant>
    {
    }
}