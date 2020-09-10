﻿namespace K1vs.DotChat.Events.Participants
{
    using System;
    using Chats;
    using K1vs.DotChat.Participants;

    public class ChatParticipantInvitedEvent: Event, IChatParticipantInvitedEvent
    {
        public ChatParticipantInvitedEvent()
        {
        }

        public ChatParticipantInvitedEvent(Guid initiatorUserId, Guid chatId, IChatParticipant participant, ChatParticipantStatus? previousStatus) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
            PreviousStatus = previousStatus;
        }

        public Guid ChatId { get; set; }
        public IChatParticipant Participant { get; set; }
        public ChatParticipantStatus? PreviousStatus { get; set; }
    }
}
