namespace K1vs.DotChat.Common.Exceptions
{
    /// <summary>
    /// 0 - 99 999
    /// </summary>
    public enum ErrorType
    {
        Unknown = 0,

        Access = ErrorFamily.Access * 1000,
        AccessDenied = Access + 10,
        AccessDeniedDueToSelfElevation = Access + 15,
        AccessDeniedDueToAdvancedProtection = Access + 15,
        AccessDeniedForeign = Access + 15,

        Rules = ErrorFamily.Rules * 1000,
        RulesEditImmutable = Rules + 10,

        Validation = ErrorFamily.Validation * 1000,
        NotFound = ErrorFamily.NotFound * 1000
    }
}