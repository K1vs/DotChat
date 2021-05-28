namespace K1vs.DotChat.Commands.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Chats;
    using DotChat.Participants;

    public class BulkAddInviteChatParticipantsCommand: Command, IBulkAddInviteChatParticipantsCommand
    {
        public BulkAddInviteChatParticipantsCommand()
        {
        }

        public BulkAddInviteChatParticipantsCommand(Guid initiatorUserId, Guid chatId, IParticipantsAddInviteBulk participantsAddInviteBulk) : base(initiatorUserId)
        {
            ChatId = chatId;
            ParticipantsAddInviteBulk = participantsAddInviteBulk;
        }

        public Guid ChatId { get; set; }
        public IParticipantsAddInviteBulk ParticipantsAddInviteBulk { get; set; }
    }
}
