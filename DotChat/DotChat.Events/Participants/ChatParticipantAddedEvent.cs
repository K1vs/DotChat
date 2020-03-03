namespace K1vs.DotChat.Events.Participants
{
    using System;
    using Chats;
    using K1vs.DotChat.Participants;

    public class ChatParticipantAddedEvent<TChatParticipant>: EventBase, IChatParticipantAddedEvent<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatParticipantAddedEvent()
        {
        }

        public ChatParticipantAddedEvent(Guid initiatorUserId, Guid chatId, TChatParticipant participant, ChatParticipantStatus? previousStatus) : base(initiatorUserId)
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
