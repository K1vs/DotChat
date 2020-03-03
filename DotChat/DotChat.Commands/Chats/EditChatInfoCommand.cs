namespace K1vs.DotChat.Commands.Chats
{
    using System;
    using DotChat.Chats;

    public class EditChatInfoCommand<TChatInfo> : CommandBase, IEditChatInfoCommand<TChatInfo>
        where TChatInfo : IChatInfo
    {
        public EditChatInfoCommand()
        {
        }

        public EditChatInfoCommand(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo) : base(initiatorUserId)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
        }

        public Guid ChatId { get; set; }
        public TChatInfo ChatInfo { get; set; }
    }
}
