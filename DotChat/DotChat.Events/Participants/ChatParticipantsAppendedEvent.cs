namespace K1vs.DotChat.Events.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Participants;

    public class ChatParticipantsAppendedEvent: Event, IChatParticipantsAppendedEvent
    {
        public ChatParticipantsAppendedEvent()
        {
        }

        public ChatParticipantsAppendedEvent(Guid initiatorUserId, Guid chatId, IReadOnlyCollection<IParticipationResult> added, IReadOnlyCollection<IParticipationResult> invited) : base(initiatorUserId)
        {
            ChatId = chatId;
            Added = added;
            Invited = invited;
        }

        public Guid ChatId { get; set; }
        public IReadOnlyCollection<IParticipationResult> Added { get; set; }
        public IReadOnlyCollection<IParticipationResult> Invited { get; set; }
    }
}
