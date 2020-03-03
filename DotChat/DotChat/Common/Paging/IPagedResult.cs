namespace K1vs.DotChat.Common.Paging
{
    using System.Collections.Generic;

    public interface IPagedResult<out TResultCollection, out TResult>
        where TResultCollection: IReadOnlyCollection<TResult>
    {
        TResultCollection Items { get; }
        string Previous { get; }
        string Next { get; }
    }
}
