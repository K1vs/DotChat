namespace K1vs.DotChat.Basic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bus;
    using Configuration;
    using DotChat.CommandBuilders;
    using DotChat.EventBuilders;
    using DotChat.Security;
    using DotChat.Services;
    using Models.Participants;
    using Participants;
    using Security;
    using Stores.Participants;
    using Stores.Users;
    using Workers;

    public class ChatParticipantsService : ChatParticipantsService<ChatServicesConfiguration, List<ParticipationCandidate>, ParticipationCandidate>
    {
        public ChatParticipantsService(ChatServicesConfiguration chatServicesConfiguration, IChatParticipantsPermissionValidator<List<ParticipationCandidate>, ParticipationCandidate> chatParticipantsPermissionValidator, IChatParticipantsCommandBuilder<List<ParticipationCandidate>, ParticipationCandidate> chatParticipantsCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration, chatParticipantsPermissionValidator, chatParticipantsCommandBuilder, chatCommandSender)
        {
        }
    }
}
