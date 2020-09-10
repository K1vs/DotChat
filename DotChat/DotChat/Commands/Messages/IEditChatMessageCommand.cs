namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public interface IEditChatMessageCommand : ICommand, IChatRelated, IChatMessageRelated, IHasChatMessageInfo
    {
        Guid ArchivedMessageId { get; }
    }
}
