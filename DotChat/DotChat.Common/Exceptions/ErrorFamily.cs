namespace K1vs.DotChat.Common.Exceptions
{
    /// <summary>
    /// 0 - 99
    /// </summary>
    public enum ErrorFamily
    {
        Unknown = 0,

        Access = 11, 
        Rules = 12,
        Validation = 13,
        NotFound = 14,
    }
}