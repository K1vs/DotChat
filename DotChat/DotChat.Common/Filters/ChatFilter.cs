namespace K1vs.DotChat.Common.Filters
{
    using System.Collections.Generic;
    using Chats;

    public class ChatFilter<TChatUserFilter, TMessageFilter> : IChatFilter
    {
        public ChatFilter()
        {

        }

        public ChatFilter(List<TChatUserFilter> userFiltersList, List<TMessageFilter> messageFiltersList, IReadOnlyCollection<IChatUserFilter> userFilters, IReadOnlyCollection<IMessageFilter> messageFilters, string search, bool searchInDescription)
        {
            UserFiltersList = userFiltersList;
            MessageFiltersList = messageFiltersList;
            UserFilters = userFilters;
            MessageFilters = messageFilters;
            Search = search;
            SearchInDescription = searchInDescription;
        }

        public List<TChatUserFilter> UserFiltersList { get; set; }
        public List<TMessageFilter> MessageFiltersList { get; set; }
        public IReadOnlyCollection<IChatUserFilter> UserFilters { get; set; }
        public IReadOnlyCollection<IMessageFilter> MessageFilters { get; set; }
        public string Search { get; set; }
        public bool SearchInDescription { get; set; }
    }
}
