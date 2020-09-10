namespace K1vs.DotChat.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IPersonalizedChat
        : IChat
    {
        long ReadIndex { get; }
        long UnreadCount { get; }
    }
}
