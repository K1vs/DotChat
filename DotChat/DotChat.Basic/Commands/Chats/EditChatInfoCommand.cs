namespace K1vs.DotChat.Basic.Commands.Chats
{
    using System;
    using DotChat.Chats;
    using DotChat.Commands.Chats;
    using Models.Chats;

    public class EditChatInfoCommand: EditChatInfoCommand<ChatInfo>
    {
        public EditChatInfoCommand()
        {
        }

        public EditChatInfoCommand(Guid initiatorUserId, Guid chatId, ChatInfo chatInfo) : base(initiatorUserId, chatId, chatInfo)
        {
        }
    }
}
