namespace K1vs.DotChat.Common.Filters
{
    using System.Collections.Generic;
    using Chats;

    public interface IChatFilter
    {
        IReadOnlyCollection<IChatParticipantFilter> ParticipantFilters { get; }
        IReadOnlyCollection<IChatMessageFilter> MessageFilters { get;  }
        string Search { get; }
        bool SearchInDescription { get; }
    }
}
