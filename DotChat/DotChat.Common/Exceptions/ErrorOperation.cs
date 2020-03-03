namespace K1vs.DotChat.Common.Exceptions
{
    /// <summary>
    /// 0 - 99
    /// </summary>
    public enum ErrorOperation
    {
        Unknown = 0,

        Get = 10,
        GetList = 11,
        Add = 15,
        Edit = 20,
        Remove = 25,
        Aggregate = 30,
        Mark = 35
    }
}