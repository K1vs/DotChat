namespace K1vs.DotChat.Messages
{
    using System.Collections.Generic;
    using Chats;
    using Participants;
    using Typed;

    public interface IHasChatMessageInfo
    {
        IChatMessageInfo MessageInfo { get; }
    }
}
