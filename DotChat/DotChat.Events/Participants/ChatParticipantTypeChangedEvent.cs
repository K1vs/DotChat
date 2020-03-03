namespace K1vs.DotChat.Events.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Participants;

    public class ChatParticipantTypeChangedEvent<TChatParticipant>: EventBase, IChatParticipantTypeChangedEvent<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatParticipantTypeChangedEvent()
        {
        }

        public ChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId, TChatParticipant participant) : base(initiatorUserId)
        {
            ChatId = chatId;
            Participant = participant;
        }

        public Guid ChatId { get; set; }
        public TChatParticipant Participant { get; set; }
    }
}
