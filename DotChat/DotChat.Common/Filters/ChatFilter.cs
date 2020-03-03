namespace K1vs.DotChat.Common.Filters
{
    using System.Collections.Generic;
    using Chats;

    public class ChatFilter<TChatUserFilter, TMessageFilter> : IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
    {
        public List<TChatUserFilter> UserFiltersList { get; set; }
        public List<TMessageFilter> MessageFiltersList { get; set; }
        public IReadOnlyCollection<TChatUserFilter> UserFilters => UserFiltersList;
        public IReadOnlyCollection<TMessageFilter> MessageFilters => MessageFiltersList;
        public string Search { get; set; }
        public bool SearchInDescription { get; set; }
    }
}
