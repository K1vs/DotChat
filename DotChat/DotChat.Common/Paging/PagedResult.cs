namespace K1vs.DotChat.Common.Paging
{
    using System.Collections.Generic;

    public class PagedResult<TResult> : IPagedResult<TResult>
    {
        public PagedResult()
        {
        }

        public PagedResult(IReadOnlyCollection<TResult> items, string previous, string next)
        {
            Items = items;
            Previous = previous;
            Next = next;
        }

        public IReadOnlyCollection<TResult> Items { get; }
        public string Previous { get; set; }
        public string Next { get; set; }
    }
}
