namespace K1vs.DotChat.Events.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Chats;
    using DotChat.Messages;
    using K1vs.DotChat.Events.Messages;

    public interface IChatParticipantReadEvent: IChatMessageEvent, IChatRelated, IIndexed
    {
        bool Force { get; }
    }
}
