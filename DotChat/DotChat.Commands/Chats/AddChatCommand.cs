namespace K1vs.DotChat.Commands.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;

    public class AddChatCommand: Command, IAddChatCommand
    {
        public AddChatCommand()
        {
        }

        public AddChatCommand(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo, IReadOnlyCollection<IParticipationCandidate> add, IReadOnlyCollection<IParticipationCandidate> invite) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
            ToAdd = add;
            ToInvite = invite;
        }

        public Guid ChatId { get; set; }

        public IChatInfo ChatInfo { get; set; }

        public IReadOnlyCollection<IParticipationCandidate> ToAdd { get; set; }

        public IReadOnlyCollection<IParticipationCandidate> ToInvite { get; set; }
    }
}
