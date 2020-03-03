namespace K1vs.DotChat.Basic.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using Basic.Messages;
    using Basic.Messages.Typed;
    using DotChat.Chats;
    using DotChat.Commands.Messages;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public class AddChatMessageCommand: AddChatMessageCommand<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public AddChatMessageCommand()
        {
        }

        public AddChatMessageCommand(Guid initiatorUserId, Guid chatId, Guid messageId, long index, ChatMessageInfo messageInfo) : base(initiatorUserId, chatId, messageId, index, messageInfo)
        {
        }
    }
}
