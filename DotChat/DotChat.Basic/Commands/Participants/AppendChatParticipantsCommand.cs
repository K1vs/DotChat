namespace K1vs.DotChat.Basic.Commands.Participants
{
    using System;
    using System.Collections.Generic;
    using DotChat.Commands.Participants;
    using DotChat.Participants;
    using Models.Participants;

    public class AppendChatParticipantsCommand: AppendChatParticipantsCommand<List<ParticipationCandidate>, ParticipationCandidate>
    {
        public AppendChatParticipantsCommand()
        {
        }

        public AppendChatParticipantsCommand(Guid initiatorUserId, Guid chatId, List<ParticipationCandidate> add, List<ParticipationCandidate> invite) : base(initiatorUserId, chatId, add, invite)
        {
        }
    }
}
