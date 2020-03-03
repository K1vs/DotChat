namespace K1vs.DotChat.Common.Paging
{
    using System.Collections.Generic;

    public class PagedResult<TResultCollection, TResult> : IPagedResult<TResultCollection ,TResult>
        where TResultCollection : IReadOnlyCollection<TResult>
    {
        public PagedResult()
        {
        }

        public PagedResult(TResultCollection items, string previous, string next)
        {
            Items = items;
            Previous = previous;
            Next = next;
        }

        public TResultCollection Items { get; }
        public string Previous { get; set; }
        public string Next { get; set; }
    }
}
