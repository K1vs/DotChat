namespace K1vs.DotChat.Commands.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IAddChatCommand: ICommand, IChatRelated, IHasChatInfo, IHasParticipationCandidates
    {
    }
}
