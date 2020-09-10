namespace K1vs.DotChat.Commands.Messages
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public interface IAddChatMessageCommand: ICommand, IChatRelated, IChatMessageRelated, IHasTimestamp, IIndexed, IHasChatMessageInfo
    {
        bool IsSystem { get; }
    }
}
