namespace K1vs.DotChat.Messages.Typed
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Common;
    using Participants;

    public interface IQuoteMessage: ICustomizable, IChatRelated, IChatMessageRelated, IHasTimestamp, IHasChatMessageInfo
    {
    }
}
