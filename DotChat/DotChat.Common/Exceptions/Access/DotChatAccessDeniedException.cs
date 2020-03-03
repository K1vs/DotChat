namespace K1vs.DotChat.Common.Exceptions.Access
{
    using System;

    public class DotChatAccessDeniedException: DotChatAccessException
    {
        public DotChatAccessDeniedException(ErrorCode errorCode, string serviceName, string methodName, Guid? chatId, Guid? userId, Exception innerException = null)
            : base(new ErrorInfo(errorCode, $"User ({userId}) has no access to {methodName} in {serviceName}.", chatId, userId, null), innerException)
        {
        }
    }
}
