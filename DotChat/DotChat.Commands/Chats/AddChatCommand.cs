namespace K1vs.DotChat.Commands.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;

    public class AddChatCommand: Command, IAddChatCommand
    {
        public AddChatCommand()
        {
        }

        public AddChatCommand(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo, IParticipantsAddInviteBulk participantsAddInviteBulk) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
            ParticipantsAddInviteBulk = participantsAddInviteBulk;
        }

        public Guid ChatId { get; set; }

        public IChatInfo ChatInfo { get; set; }

        public IParticipantsAddInviteBulk ParticipantsAddInviteBulk { get; set; }
    }
}
