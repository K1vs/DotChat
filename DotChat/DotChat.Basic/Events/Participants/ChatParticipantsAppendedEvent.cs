namespace K1vs.DotChat.Basic.Events.Participants
{
    using System;
    using System.Collections.Generic;
    using Basic.Participants;
    using DotChat.Events.Participants;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatParticipantsAppendedEvent: ChatParticipantsAppendedEvent<List<ParticipationResult>, ParticipationResult, ChatParticipant>
    {
        public ChatParticipantsAppendedEvent()
        {
        }

        public ChatParticipantsAppendedEvent(Guid initiatorUserId, Guid chatId, List<ParticipationResult> added, List<ParticipationResult> invited) : base(initiatorUserId, chatId, added, invited)
        {
        }
    }
}
