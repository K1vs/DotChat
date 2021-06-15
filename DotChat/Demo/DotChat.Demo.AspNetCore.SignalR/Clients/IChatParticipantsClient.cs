namespace K1vs.DotChat.Demo.AspNetCore.SignalR.Clients
{
    using System.Collections.Generic;
    using Basic.Participants;
    using Implementations.AspNetCore.SignalR;
    using Models.Participants;

    public interface IChatParticipantsClient: IChatParticipantsClient<List<ParticipationResult>, ParticipationResult, ChatParticipant>
    {
    }
}