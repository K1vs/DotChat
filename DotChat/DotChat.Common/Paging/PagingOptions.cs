namespace K1vs.DotChat.Common.Paging
{
    public class PagingOptions: IPagingOptions
    {
        public PagingOptions()
        {
        }

        public PagingOptions(string cursor, int limit)
        {
            Cursor = cursor;
            Limit = limit;
        }

        public string Cursor { get; set; }
        public int Limit { get; set; }
    }
}
