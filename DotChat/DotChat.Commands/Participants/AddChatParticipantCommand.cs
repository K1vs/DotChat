namespace K1vs.DotChat.Commands.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;

    public class AddChatParticipantCommand: CommandBase, IAddChatParticipantCommand
    {
        public AddChatParticipantCommand()
        {
        }

        public AddChatParticipantCommand(Guid initiatorUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null) : base(initiatorUserId)
        {
            ChatId = chatId;
            UserId = userId;
            ChatParticipantType = chatParticipantType;
            Style = style;
            Metadata = metadata;
        }

        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public ChatParticipantType ChatParticipantType { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}
