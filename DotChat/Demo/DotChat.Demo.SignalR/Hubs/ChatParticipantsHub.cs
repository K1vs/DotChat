namespace K1vs.DotChat.Demo.SignalR.Hubs
{
    using System.Collections.Generic;
    using Basic.Participants;
    using Clients;
    using K1vs.DotChat.Implementations.SignalR;
    using Models.Participants;
    using Services;

    public class ChatParticipantsHub: ChatParticipantsHub<IChatParticipantsClient, List<ParticipationModificationResult>, ParticipationModificationResult, ChatParticipant, List<ParticipationCandidate>, ParticipationCandidate>
    {
        public ChatParticipantsHub(IChatParticipantsService<List<ParticipationCandidate>, ParticipationCandidate> chatParticipantsService) : base(chatParticipantsService)
        {
        }
    }
}