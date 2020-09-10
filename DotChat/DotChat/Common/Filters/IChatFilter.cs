namespace K1vs.DotChat.Common.Filters
{
    using System.Collections.Generic;
    using Chats;

    public interface IChatFilter
    {
        IReadOnlyCollection<IChatUserFilter> UserFilters { get; }
        IReadOnlyCollection<IMessageFilter> MessageFilters { get;  }
        string Search { get; }
        bool SearchInDescription { get; }
    }
}
