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

    public class EditChatMessageCommand: EditChatMessageCommand<ChatInfo, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public EditChatMessageCommand()
        {
        }

        public EditChatMessageCommand(Guid initiatorUserId, Guid chatId, Guid messageId, ChatMessageInfo messageInfo, Guid archivedMessageId) : base(initiatorUserId, chatId, messageId, messageInfo, archivedMessageId)
        {
        }
    }
}
