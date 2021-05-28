namespace K1vs.DotChat.Common.Filters
{
    using System.Collections.Generic;
    using Chats;

    public class ChatFilter<TChatUserFilter, TMessageFilter> : IChatFilter
    {
        public ChatFilter()
        {

        }

        public ChatFilter(IReadOnlyCollection<IChatParticipantFilter> participantFilters, IReadOnlyCollection<IChatMessageFilter> messageFilters, string search, bool searchInDescription)
        {
            ParticipantFilters = participantFilters;
            MessageFilters = messageFilters;
            Search = search;
            SearchInDescription = searchInDescription;
        }

        public IReadOnlyCollection<IChatParticipantFilter> ParticipantFilters { get; set; }
        public IReadOnlyCollection<IChatMessageFilter> MessageFilters { get; set; }
        public string Search { get; set; }
        public bool SearchInDescription { get; set; }
    }
}
