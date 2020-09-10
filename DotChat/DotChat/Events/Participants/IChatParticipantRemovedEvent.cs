﻿namespace K1vs.DotChat.Events.Participants
{
    using Chats;
    using DotChat.Chats;
    using K1vs.DotChat.Participants;

    public interface IChatParticipantRemovedEvent: IChatParticipantEvent, IChatRelated, IParticipationResult
    {
    }
}
