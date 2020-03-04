namespace K1vs.DotChat.Commands.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Chats;
    using DotChat.Participants;

    public class ChangeChatParticipantTypeCommand: Command, IChangeChatParticipantTypeCommand
    {
        public ChangeChatParticipantTypeCommand()
        {
        }

        public ChangeChatParticipantTypeCommand(Guid initiatorUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null) : base(initiatorUserId)
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
