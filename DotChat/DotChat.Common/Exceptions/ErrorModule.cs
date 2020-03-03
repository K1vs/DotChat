namespace K1vs.DotChat.Common.Exceptions
{
    /// <summary>
    /// 0 - 99
    /// </summary>
    public enum ErrorModule
    {
        Unknown = 0,

        Service = 10,
        Worker = 15,
        Bus = 20,
        Store = 25,
        Notifier = 30,
        Security = 35
    }
}