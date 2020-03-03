namespace K1vs.DotChat.Basic.Events.Participants
{
    using System;
    using DotChat.Events.Participants;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatParticipantAddedEvent: ChatParticipantAddedEvent<ChatParticipant>
    {
        public ChatParticipantAddedEvent()
        {
        }

        public ChatParticipantAddedEvent(Guid initiatorUserId, Guid chatId, ChatParticipant participant, ChatParticipantStatus? previousStatus) : base(initiatorUserId, chatId, participant, previousStatus)
        {
        }
    }
}
