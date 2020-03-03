namespace K1vs.DotChat.Common.Filters
{
    using System.Collections.Generic;
    using Chats;

    public interface IChatFilter<out TChatUserFilter, out TMessageFilter>
        where TChatUserFilter: IChatUserFilter
        where TMessageFilter: IMessageFilter
    {
        IReadOnlyCollection<TChatUserFilter> UserFilters { get; }
        IReadOnlyCollection<TMessageFilter> MessageFilters { get;  }
        string Search { get; }
        bool SearchInDescription { get; }
    }
}
