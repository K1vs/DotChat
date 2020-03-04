namespace K1vs.DotChat.Events.Participants
{
    using System;
    using Chats;
    using K1vs.DotChat.Participants;

    public class ChatParticipantAppliedEvent<TChatParticipant>: Event, IChatParticipantAppliedEvent<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatParticipantAppliedEvent()
        {
        }

        public ChatParticipantAppliedEvent(Guid initiatorUserId, Guid chatId, TChatParticipant participant, ChatParticipantStatus? previousStatus) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
            PreviousStatus = previousStatus;
        }

        public Guid ChatId { get; set; }
        public TChatParticipant Participant { get; set; }
        public ChatParticipantStatus? PreviousStatus { get; set; }
    }
}
