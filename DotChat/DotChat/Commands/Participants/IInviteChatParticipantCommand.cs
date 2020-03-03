﻿namespace K1vs.DotChat.Commands.Participants
{
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IInviteChatParticipantCommand : ICommandBase, IChatRelated, IParticipationCandidate
    {
    }
}
