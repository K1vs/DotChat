namespace K1vs.DotChat.Common.Filters
{
    public class ChatUserUserFilter : IChatUserUserFilter
    {
        public ChatUserUserFilter()
        {
        }

        public ChatUserUserFilter(string search)
        {
            Search = search;
        }

        public string Search { get; set; }
    }
}
