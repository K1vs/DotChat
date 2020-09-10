namespace K1vs.DotChat.Messages.Typed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Common;

    public interface IChatRefMessage: ICustomizable, IChatRelated
    {
        IChatInfo ChatInfo { get; }
    }
}
