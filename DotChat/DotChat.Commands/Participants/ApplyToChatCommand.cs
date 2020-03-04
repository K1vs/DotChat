namespace K1vs.DotChat.Commands.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;

    public class ApplyToChatCommand: Command, IApplyToChatCommand
    {
        public ApplyToChatCommand()
        {
        }

        public ApplyToChatCommand(Guid initiatorUserId, Guid chatId, ChatParticipantType chatParticipantType, string style = null, string metadata = null) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatParticipantType = chatParticipantType;
            Style = style;
            Metadata = metadata;
        }

        public Guid ChatId { get; set; }
        public ChatParticipantType ChatParticipantType { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}
