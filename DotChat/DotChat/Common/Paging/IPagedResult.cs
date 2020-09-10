namespace K1vs.DotChat.Common.Paging
{
    using System.Collections.Generic;

    public interface IPagedResult<out TResult>
    {
        IReadOnlyCollection<TResult> Items { get; }
        string Previous { get; }
        string Next { get; }
    }
}
