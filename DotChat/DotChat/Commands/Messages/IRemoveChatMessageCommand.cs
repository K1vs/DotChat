﻿namespace K1vs.DotChat.Commands.Messages
{
    using DotChat.Chats;
    using DotChat.Messages;

    public interface IRemoveChatMessageCommand: ICommandBase, IChatRelated, IChatMessageRelated
    {
    }
}
