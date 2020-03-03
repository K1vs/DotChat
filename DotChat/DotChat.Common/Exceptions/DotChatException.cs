namespace K1vs.DotChat.Common.Exceptions
{
    using System;

    public class DotChatException: Exception
    {
        public ErrorInfo Error { get; }

        public DotChatException(ErrorInfo error)
            : this(error, null)
        {
            Error = error;
        }

        public DotChatException(ErrorInfo error, Exception innerException)
            :base(error.Message, innerException)
        {

        }
    }
}
