namespace K1vs.DotChat.Commands.Messages
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public interface IIndexChatMessageCommand : ICommand, IChatRelated, IChatMessageRelated, IHasChatMessageInfo
    {
        bool IsSystem { get; }
    }
}
