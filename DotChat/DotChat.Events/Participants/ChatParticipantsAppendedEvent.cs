namespace K1vs.DotChat.Events.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Participants;

    public class ChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant>: EventBase, IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant>
        where TParticipationResultCollection: IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        public ChatParticipantsAppendedEvent()
        {
        }

        public ChatParticipantsAppendedEvent(Guid initiatorUserId, Guid chatId, TParticipationResultCollection added, TParticipationResultCollection invited) : base(initiatorUserId)
        {
            ChatId = chatId;
            Added = added;
            Invited = invited;
        }

        public Guid ChatId { get; set; }
        public TParticipationResultCollection Added { get; set; }
        public TParticipationResultCollection Invited { get; set; }
    }
}
