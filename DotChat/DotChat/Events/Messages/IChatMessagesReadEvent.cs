namespace K1vs.DotChat.Events.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Chats;
    using DotChat.Messages;

    public interface IChatMessagesReadEvent: IChatMessageEvent, IChatRelated, IIndexed
    {
    }
}
