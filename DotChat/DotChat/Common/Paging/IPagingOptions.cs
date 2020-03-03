namespace K1vs.DotChat.Common.Paging
{
    public interface IPagingOptions
    {
        string Cursor { get; }
        int Limit { get; }
    }
}
