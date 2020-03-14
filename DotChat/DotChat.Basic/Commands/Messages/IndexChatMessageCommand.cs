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

    public class IndexChatMessageCommand: IndexChatMessageCommand<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public IndexChatMessageCommand()
        {
        }

        public IndexChatMessageCommand(Guid initiatorUserId, Guid chatId, Guid messageId, bool isSystem, ChatMessageInfo messageInfo) : base(initiatorUserId, chatId, messageId, isSystem, messageInfo)
        {
        }
    }
}
