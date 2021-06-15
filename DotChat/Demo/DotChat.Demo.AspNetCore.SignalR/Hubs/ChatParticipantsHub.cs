namespace K1vs.DotChat.Demo.AspNetCore.SignalR.Hubs
{
    using System.Collections.Generic;
    using Basic.Participants;
    using Clients;
    using Implementations.AspNetCore.SignalR;
    using Models.Participants;
    using Services;

    public class ChatParticipantsHub: ChatParticipantsHub<IChatParticipantsClient, List<ParticipationResult>, ParticipationResult, ChatParticipant, List<ParticipationCandidate>, ParticipationCandidate>
    {
        public ChatParticipantsHub(IChatParticipantsService<List<ParticipationCandidate>, ParticipationCandidate> chatParticipantsService) : base(chatParticipantsService)
        {
        }
    }
}