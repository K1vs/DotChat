namespace K1vs.DotChat.Basic.Commands.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Commands.Chats;
    using DotChat.Participants;
    using Models.Chats;
    using Models.Participants;

    public class AddChatCommand: AddChatCommand<ChatInfo, List<ParticipationCandidate>, ParticipationCandidate>
    {
        public AddChatCommand()
        {
        }

        public AddChatCommand(Guid initiatorUserId, Guid chatId, ChatInfo chatInfo, List<ParticipationCandidate> add, List<ParticipationCandidate> invite) : base(initiatorUserId, chatId, chatInfo, add, invite)
        {
        }
    }
}
