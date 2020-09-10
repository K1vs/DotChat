namespace K1vs.DotChat.Commands.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Chats;
    using DotChat.Participants;

    public class AppendChatParticipantsCommand: Command, IAppendChatParticipantsCommand
    {
        public AppendChatParticipantsCommand()
        {
        }

        public AppendChatParticipantsCommand(Guid initiatorUserId, Guid chatId, IReadOnlyCollection<IParticipationCandidate> add, IReadOnlyCollection<IParticipationCandidate> invite) : base(initiatorUserId)
        {
            ChatId = chatId;
            ToAdd = add;
            ToInvite = invite;
        }

        public Guid ChatId { get; set; }
        public IReadOnlyCollection<IParticipationCandidate> ToAdd { get; set; }
        public IReadOnlyCollection<IParticipationCandidate> ToInvite { get; set; }
    }
}
