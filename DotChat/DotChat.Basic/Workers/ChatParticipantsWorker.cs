namespace K1vs.DotChat.Basic.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Configuration;
    using DotChat.EventBuilders;
    using DotChat.Security;
    using DotChat.Workers;
    using Models.Participants;
    using Participants;
    using Security;
    using Stores.Participants;
    using Stores.Users;

    public class ChatParticipantsWorker: ChatParticipantsWorker<ChatWorkersConfiguration, List<ParticipationResult>, ParticipationResult, ChatParticipant, List<ParticipationCandidate>, ParticipationCandidate, ChatUser>
    {
        public ChatParticipantsWorker(ChatWorkersConfiguration chatWorkersConfiguration, IChatParticipantsPermissionValidator<List<ParticipationCandidate>, ParticipationCandidate> chatParticipantsPermissionValidator, IChatParticipantStore<ChatParticipant, ChatUser> chatParticipantStore, IReadUserStore<ChatUser> readUserStore, IChatParticipantsEventBuilder<List<ParticipationResult>, ParticipationResult, ChatParticipant> chatParticipantsEventBuilder) : base(chatWorkersConfiguration, chatParticipantsPermissionValidator, chatParticipantStore, readUserStore, chatParticipantsEventBuilder)
        {
        }
    }
}
