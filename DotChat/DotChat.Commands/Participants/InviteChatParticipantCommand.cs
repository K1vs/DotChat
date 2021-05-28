﻿namespace K1vs.DotChat.Commands.Participants
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;

    public class InviteChatParticipantCommand: Command, IInviteChatParticipantCommand
    {
        public InviteChatParticipantCommand()
        {
        }

        public InviteChatParticipantCommand(Guid initiatorUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, IReadOnlyList<string> styles = null) : base(initiatorUserId)
        {
            ChatId = chatId;
            UserId = userId;
            ChatParticipantType = chatParticipantType;
            Styles = styles;
        }

        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public ChatParticipantType ChatParticipantType { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}
