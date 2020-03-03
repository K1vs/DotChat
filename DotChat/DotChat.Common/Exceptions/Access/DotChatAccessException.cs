namespace K1vs.DotChat.Common.Exceptions.Access
{
    using System;

    public class DotChatAccessException: DotChatException
    {
        public DotChatAccessException(ErrorInfo error) : base(error)
        {
        }

        public DotChatAccessException(ErrorInfo error, Exception innerException) : base(error, innerException)
        {
        }
    }
}
