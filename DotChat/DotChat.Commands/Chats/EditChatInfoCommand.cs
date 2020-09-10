namespace K1vs.DotChat.Commands.Chats
{
    using System;
    using DotChat.Chats;

    public class EditChatInfoCommand: Command, IEditChatInfoCommand
    {
        public EditChatInfoCommand()
        {
        }

        public EditChatInfoCommand(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
        }

        public Guid ChatId { get; set; }

        public IChatInfo ChatInfo { get; set; }
    }
}
