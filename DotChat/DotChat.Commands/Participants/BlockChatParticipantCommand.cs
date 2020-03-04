namespace K1vs.DotChat.Commands.Participants
{
    using System;
    using DotChat.Chats;
    using DotChat.Participants;

    public class BlockChatParticipantCommand: Command, IBlockChatParticipantCommand
    {
        public BlockChatParticipantCommand()
        {
        }

        public BlockChatParticipantCommand(Guid initiatorUserId, Guid chatId, Guid userId) : base(initiatorUserId)
        {
            ChatId = chatId;
            UserId = userId;
        }

        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
    }
}
