namespace K1vs.DotChat.Basic.Events.Participants
{
    using System;
    using DotChat.Events.Participants;
    using DotChat.Participants;
    using Models.Participants;

    public class ChatParticipantTypeChangedEvent: ChatParticipantTypeChangedEvent<ChatParticipant>
    {
        public ChatParticipantTypeChangedEvent()
        {
        }

        public ChatParticipantTypeChangedEvent(Guid initiatorUserId, Guid chatId, ChatParticipant participant) : base(initiatorUserId, chatId, participant)
        {
        }
    }
}
